using Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace Application.IntegrationTests.TestHelpers;

public class TestingDatabaseFixture : IDisposable
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly Respawner _checkpoint;
    private readonly string _connectionString;

    public const string DatabaseCollectionDefinition = "Database collection";

    public TestingDatabaseFixture()
    {
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        
        IConfiguration configuration = _factory.Services.GetRequiredService<IConfiguration>();

        _connectionString = configuration.GetConnectionString("DefaultConnection")
                           ?? throw new ArgumentNullException(nameof(_connectionString), "Missing connection string");

        _checkpoint = Respawner.CreateAsync(
            _connectionString,
            new RespawnerOptions
            {
                TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
            }).GetAwaiter().GetResult();
    }

    public void Dispose()
    {
    }
    
    public async Task ResetState()
    {
        await _checkpoint.ResetAsync(_connectionString);
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }
    
}

[CollectionDefinition(TestingDatabaseFixture.DatabaseCollectionDefinition)]
public class DatabaseCollection : ICollectionFixture<TestingDatabaseFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

