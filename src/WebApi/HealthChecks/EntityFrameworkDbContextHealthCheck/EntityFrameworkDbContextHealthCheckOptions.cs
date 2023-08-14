using Microsoft.EntityFrameworkCore;

namespace SSW.CleanArchitecture.WebApi.HealthChecks.EntityFrameworkDbContextHealthCheck;

public sealed class EntityFrameworkDbContextHealthCheckOptions<TContext> where TContext : DbContext
{
    public Func<TContext, CancellationToken, Task<DbHealthCheckResult>>? TestQuery { get; set; }
}
