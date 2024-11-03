using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AppHost.Commands;

public static class SqlServerCommandExt
{
    public static IResourceBuilder<SqlServerServerResource> WithDropDatabaseCommand(
        this IResourceBuilder<SqlServerServerResource> builder)
    {
        builder.WithCommand(
            "drop-database",
            "Drop Database",
            async context =>
            {
                var connectionString = await builder.Resource.GetConnectionStringAsync();
                ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

                var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
                optionsBuilder.UseSqlServer(connectionString);
                var db = new DbContext(optionsBuilder.Options);
                await db.Database.EnsureDeletedAsync();

                return CommandResults.Success();
            },
            context =>
            {
                if (context.ResourceSnapshot.HealthStatus is HealthStatus.Healthy)
                {
                    return ResourceCommandState.Enabled;
                }

                return ResourceCommandState.Disabled;
            });
        return builder;
    }
}