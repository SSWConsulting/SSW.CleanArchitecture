using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SSW.CleanArchitecture.Infrastructure.Persistence;
using SSW.CleanArchitecture.WebApi.HealthChecks.EntityFrameworkDbContextHealthCheck;

namespace SSW.CleanArchitecture.WebApi.HealthChecks;

public static class WebApplicationHealthCheckExtensions
{
    public static WebApplication UseHealthChecks(this WebApplication app)
    {
        // Basic Healthy/Degraded/Unhealthy result
        app.UseHealthChecks("/health");

        // Detailed Report about each check
        // TODO: Because of the detailed information, this endpoint should be secured behind
        // an Authorization Policy (.RequireAuthorization()), or a specific secured port through firewall rules
        app.UseHealthChecks("/health-report",
            new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        return app;
    }

    public static void AddHealthChecks(this IServiceCollection services, IConfiguration config)
    {
        services.AddHealthChecks()
            // Check 1: Check the SQL Server Connectivity (no EF, no DBContext, hardly anything to go wrong)
            .AddSqlServer(
                name: "SQL Server",
                connectionString: config.GetConnectionString("CleanArchitecture")!,
                healthQuery: $"-- SqlServerHealthCheck{Environment.NewLine}SELECT 123;",
                failureStatus: HealthStatus.Unhealthy,
                tags: ["db", "sql", "sqlserver"])

            // Check 2: Check the Entity Framework DbContext (requires the DbContext Options, DI, Interceptors, Configurations, etc. to all be correct), and
            // then run a query to test pending migrations
            .AddEntityFrameworkDbContextCheck<ApplicationDbContext>(
                name: "Entity Framework DbContext",
                tags: ["db", "dbContext", "sql"],
                testQuery: async (ctx, ct) => (await ctx.Database.GetPendingMigrationsAsync(ct)).Any()
                        ? new DbHealthCheckResult("There are pending database migrations. Please apply them first.", exception: null)
                        : new DbHealthCheckResult("All migrations are applied")
            );
    }
}