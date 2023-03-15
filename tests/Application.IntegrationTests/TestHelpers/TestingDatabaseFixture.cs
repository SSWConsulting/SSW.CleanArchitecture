using Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace Application.IntegrationTests.TestHelpers;

public class TestingDatabaseFixture : IAsyncLifetime
{
    private readonly IntegrationTestWebApplicationFactory _factory;
    private readonly IServiceScopeFactory _scopeFactory;
    private Respawner _checkpoint = default!;
    private readonly string _connectionString;

    public const string DatabaseCollectionDefinition = "Database collection";

    public TestingDatabaseFixture()
    {
        _factory = new IntegrationTestWebApplicationFactory();
        _factory.DatabaseFixture.InitializeAsync().GetAwaiter().GetResult();
        _connectionString = _factory.DatabaseFixture.ConnectionString
                            ?? throw new ArgumentNullException(nameof(_factory.DatabaseFixture.ConnectionString), "Missing connection string!");
        
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

    }

    public async Task InitializeAsync()
    {
        _checkpoint = await Respawner.CreateAsync(_connectionString);
    }

    public async Task DisposeAsync()
    {
        await _factory.DatabaseFixture.DisposeAsync();
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

