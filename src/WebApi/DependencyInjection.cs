using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Infrastructure.Persistence;
using SSW.CleanArchitecture.WebApi.HealthChecks.EntityFrameworkDbContextHealthCheck;
using SSW.CleanArchitecture.WebApi.Services;

namespace SSW.CleanArchitecture.WebApi;

public static class DependencyInjection
{
    public static void AddWebApi(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddOpenApiDocument(configure => configure.Title = "CleanArchitecture API");
        services.AddEndpointsApiExplorer();

        AddHealthChecks(services, config);
    }

    private static void AddHealthChecks(IServiceCollection services, IConfiguration config)
    {
        services.AddHealthChecks()
            // Check 1: Check the SQL Server Connectivity (no EF, no DBContext, hardly anything to go wrong)
            .AddSqlServer(
                name: "SQL Server",
                connectionString: config["ConnectionStrings:DefaultConnection"]!,
                healthQuery: $"-- SqlServerHealthCheck{Environment.NewLine}SELECT 123;",
                failureStatus: HealthStatus.Unhealthy,
                tags: new string[] { "db", "sql", "sqlserver" })

            // Check 2: Check the Entity Framework DbContext (requires the DbContext Options, DI, Interceptors, Configurations, etc. to all be correct), and 
            // then run a sample query to test important data
            // Note: Add TagWith("HealthCheck") to show up in SQL Profiling tools (usually as the opening comment) so that you know that the constant DB Queries are
            // for the health check of the current application and not something strange/unidentified.
            .AddEntityFrameworkDbContextCheck<ApplicationDbContext>(
                name: "Entity Framework DbContext",
                tags: new string[] { "db", "dbContext", "sql" },
                testQuery: async (ctx, ct) =>
                {
                    // TODO: Replace the custom test query below with something appropriate for your project that is always expected to be valid
                    _ = await ctx
                        .TodoItems
                        // allows you to understand why you might see constant db queries in sql profile
                        .TagWith("HealthCheck")
                        .FirstOrDefaultAsync(ct);

                    return new DbHealthCheckResult("Database Context is healthy");
                });
    }
}