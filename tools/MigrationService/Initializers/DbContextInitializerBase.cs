using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace MigrationService.Initializers;

internal abstract class DbContextInitializerBase<T> where T : DbContext
{
    protected readonly T DbContext;

    // public constructor needed for DI
    internal DbContextInitializerBase(T dbContext)
    {
        DbContext = dbContext;
    }

    internal async Task EnsureDatabaseAsync(CancellationToken cancellationToken)
    {
        var dbCreator = DbContext.GetService<IRelationalDatabaseCreator>();

        var strategy = DbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Create the database if it does not exist.
            // Do this first so there is then a database to start a transaction against.
            if (!await dbCreator.ExistsAsync(cancellationToken))
            {
                await dbCreator.CreateAsync(cancellationToken);
            }
        });
    }

    internal async Task RunMigrationAsync(CancellationToken cancellationToken)
    {
        var strategy = DbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            // await using var transaction = await DbContext.Database.BeginTransactionAsync(cancellationToken);
            await DbContext.Database.MigrateAsync(cancellationToken);
            // await transaction.CommitAsync(cancellationToken);
        });
    }
}