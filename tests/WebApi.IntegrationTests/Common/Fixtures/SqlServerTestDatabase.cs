using Microsoft.EntityFrameworkCore;
using Respawn;
using SSW.CleanArchitecture.Infrastructure.Persistence;
using System.Data.Common;

namespace WebApi.IntegrationTests.Common.Fixtures;

public class SqlServerTestDatabase : IAsyncDisposable
{
    private static DatabaseContainer _database = new();
    private Respawner _checkpoint = null!;

    /// <summary>
    /// Create and seed database
    /// </summary>
    public async Task InitializeAsync()
    {
        await _database.InitializeAsync();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(_database.ConnectionString)
            .Options;

        await using var dbContext = new ApplicationDbContext(options);
        await dbContext.Database.MigrateAsync();

        _checkpoint = await Respawner.CreateAsync(_database.ConnectionString,
            new RespawnerOptions { TablesToIgnore = ["__EFMigrationsHistory"] });
    }

    public DbConnection GetConnection() => _database.ConnectionString;

    public async Task ResetAsync()
    {
        await _checkpoint.ResetAsync(_database.ConnectionString);
    }

    public async ValueTask DisposeAsync()
    {
        await _database.DisposeAsync();
    }
}