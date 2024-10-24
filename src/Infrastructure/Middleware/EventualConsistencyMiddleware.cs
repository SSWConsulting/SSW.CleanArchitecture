using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SSW.CleanArchitecture.Domain.Common.EventualConsistency;
using SSW.CleanArchitecture.Domain.Common.Interfaces;
using SSW.CleanArchitecture.Infrastructure.Persistence;

namespace SSW.CleanArchitecture.Infrastructure.Middleware;

public class EventualConsistencyMiddleware
{
    public const string DomainEventsKey = "DomainEventsKey";

    private readonly RequestDelegate _next;

    public EventualConsistencyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IPublisher publisher, ApplicationDbContext dbContext)
    {
        context.Response.OnCompleted(async () =>
        {
            if (context.Items.TryGetValue(DomainEventsKey, out var value) && value is Queue<IDomainEvent> domainEvents)
            {
                IDbContextTransaction? transaction = null;
                try
                {
                    var strategy = dbContext.Database.CreateExecutionStrategy();
                    await strategy.ExecuteAsync(async () =>
                    {
                        transaction = await dbContext.Database.BeginTransactionAsync();

                        while (domainEvents.TryDequeue(out var nextEvent))
                            await publisher.Publish(nextEvent);

                        await transaction.CommitAsync();
                    });
                }
                catch (EventualConsistencyException)
                {
                    // handle eventual consistency exception
                }
                finally
                {
                    if (transaction is not null)
                        await transaction.DisposeAsync();
                }
            }
        });

        await _next(context);
    }
}