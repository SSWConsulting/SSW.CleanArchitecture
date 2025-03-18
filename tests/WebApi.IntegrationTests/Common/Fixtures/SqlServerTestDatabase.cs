using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Respawn;
using SSW.CleanArchitecture.Infrastructure.Persistence;
using System.Data.Common;

namespace WebApi.IntegrationTests.Common.Fixtures;

public class SqlServerTestDatabase : IAsyncDisposable
{
    private readonly DatabaseContainer _database = new();
    private Respawner _checkpoint = null!;
    private string _connectionString = null!;

    /// <summary>
    /// Create and seed database
    /// </summary>
    public async Task InitializeAsync()
    {
        await _database.InitializeAsync();

        var builder = new SqlConnectionStringBuilder(_database.Connection.ConnectionString)
        {
            InitialCatalog = "CleanArchitecture-IntegrationTests"
        };

        _connectionString = builder.ConnectionString;

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(_connectionString)
            .Options;

        using var dbContext = new ApplicationDbContext(options);
        await dbContext.Database.MigrateAsync();

        _checkpoint = await Respawner.CreateAsync(_connectionString,
            new RespawnerOptions { TablesToIgnore = ["__EFMigrationsHistory"] });
    }

    public DbConnection GetConnection() => new SqlConnection(_connectionString);

    public async Task ResetAsync()
    {
        await _checkpoint.ResetAsync(_connectionString);
    }

    public async ValueTask DisposeAsync()
    {
        await _database.DisposeAsync();
    }
}