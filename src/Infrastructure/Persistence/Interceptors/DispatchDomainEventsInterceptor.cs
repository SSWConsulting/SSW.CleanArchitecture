using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SSW.CleanArchitecture.Domain.Common.Interfaces;

namespace SSW.CleanArchitecture.Infrastructure.Persistence.Interceptors;

public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    // NOTE: There are two options for dispatching domain events:
    // Option 1. Before changes are saved to the database (SavingChanges)
    // Option 2. After changes are saved to the database (SavedChanges)
    //
    // We are using Option 2, for several reasons:
    // - Event handlers can query the DB and expect the data to be up-to-date
    // - Event handlers that fire integration events that affect other systems, will only do
    //   so if the changes are successfully saved to the database
    //
    // The downside of this is that we may have multiple calls to save changes in a single DB request.
    // This means we no longer have a single write to the DB, so may need to wrap the entire operation
    // in a transaction to ensure consistency.
    // TODO: Add ADR for this decision
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        DispatchDomainEvents(eventData.Context).ConfigureAwait(false).GetAwaiter().GetResult();
        return base.SavedChanges(eventData, result);
    }

    public async override ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        await DispatchDomainEvents(eventData.Context);
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    public async Task DispatchDomainEvents(DbContext? context)
    {
        if (context is null)
            return;

        var entities = context.ChangeTracker
            .Entries<IAggregateRoot>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}