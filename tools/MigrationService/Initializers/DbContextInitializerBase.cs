using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace MigrationService.Initializers;

public abstract class DbContextInitializerBase<T> where T : DbContext
{
    protected T DbContext { get; }

    // public constructor needed for DI
    internal DbContextInitializerBase(T dbContext)
    {
        DbContext = dbContext;
    }

    public async Task EnsureDatabaseAsync(CancellationToken cancellationToken)
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

    public async Task CreateSchemaAsync(bool useMigrations, CancellationToken cancellationToken)
    {
        var strategy = DbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            // await using var transaction = await DbContext.Database.BeginTransactionAsync(cancellationToken);

            if (useMigrations)
            {
                await DbContext.Database.MigrateAsync(cancellationToken);
            }
            else
            {
                var dbCreator = DbContext.GetService<IRelationalDatabaseCreator>();
                await dbCreator.CreateTablesAsync(cancellationToken);
            }

            // await transaction.CommitAsync(cancellationToken);
        });
    }
}