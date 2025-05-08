using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SqlServerDatabaseResource = Aspire.Hosting.ApplicationModel.SqlServerDatabaseResource;

namespace AppHost.Commands;

public static class SqlServerDatabaseCommandExt
{
    public static IResourceBuilder<SqlServerDatabaseResource> WithDropDatabaseCommand(
        this IResourceBuilder<SqlServerDatabaseResource> builder)
    {
        builder.WithCommand(
            "drop-database",
            "Drop Database",
            async _ =>
            {
                var connectionString = await builder.Resource.ConnectionStringExpression.GetValueAsync(CancellationToken.None);
                ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

                var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
                optionsBuilder.UseSqlServer(connectionString);
                var db = new DbContext(optionsBuilder.Options);
                await db.Database.EnsureDeletedAsync();

                return CommandResults.Success();
            },
            null); // Intentionally using 'null' for the command state resolver as this command does not require health status checks. Downstream code is expected to handle 'null' appropriately.
        return builder;
    }
}