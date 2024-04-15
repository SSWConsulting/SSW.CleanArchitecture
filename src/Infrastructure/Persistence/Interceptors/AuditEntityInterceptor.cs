using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Common.Interfaces;
using SSW.CleanArchitecture.Infrastructure.Audit;

namespace SSW.CleanArchitecture.Infrastructure.Persistence.Interceptors;

public class AuditEntityInterceptor : SaveChangesInterceptor
{
    private readonly AuditDbContext _auditDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly TimeProvider _timeProvider;

    public AuditEntityInterceptor(AuditDbContext auditDbContext,
        ICurrentUserService currentUserService,
        TimeProvider timeProvider)
    {
        _auditDbContext = auditDbContext;
        _currentUserService = currentUserService;
        _timeProvider = timeProvider;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var auditEntries = BuildAuditEntries(eventData.Context);
        var interceptionResult = base.SavingChanges(eventData, result);

        if (!interceptionResult.HasResult)
        {
            _auditDbContext.AddRange(auditEntries);
            _auditDbContext.SaveChanges();
        }

        return interceptionResult;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var auditEntries = BuildAuditEntries(eventData.Context);
        var interceptionResult = await base.SavingChangesAsync(eventData, result, cancellationToken);

        if (!interceptionResult.HasResult)
        {
            await _auditDbContext.AddRangeAsync(auditEntries, cancellationToken);
            await _auditDbContext.SaveChangesAsync(cancellationToken);
        }

        return interceptionResult;
    }

    private List<AuditEntry> BuildAuditEntries(DbContext? context)
    {
        if (context is null)
        {
            return [];
        }

        var now = _timeProvider.GetLocalNow();
        var userId = _currentUserService.UserId;

        var auditEntries = context.ChangeTracker
            .Entries<IAuditableEntity>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .Select(entry => AuditEntry.From(entry, userId, now))
            .OfType<AuditEntry>()
            .ToList();

        return auditEntries;
    }
}