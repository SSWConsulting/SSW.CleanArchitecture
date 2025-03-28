using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SSW.CleanArchitecture.Infrastructure.Persistence;

namespace WebApi.IntegrationTests.Common;

/// <summary>
/// Integration tests inherit from this to access helper classes
/// </summary>
[Collection<TestingDatabaseFixtureCollection>]
public abstract class IntegrationTestBase : IAsyncLifetime
{
    private readonly IServiceScope _scope;
    private readonly TestingDatabaseFixture _fixture;
    private readonly ApplicationDbContext _dbContext;

    protected IntegrationTestBase(TestingDatabaseFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        // TODO: Figure out a nice way to pass output to the factory. Ideally we would do this in via the constructor, but that's not possible as xUnit creates the fixture.
        // _fixture.Factory.Output = output;

        _scope = _fixture.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    /// <summary>
    /// Setup for each test
    /// </summary>
    public async ValueTask InitializeAsync()
    {
        await _fixture.TestSetup();
    }

    protected IQueryable<T> GetQueryable<T>() where T : class => _dbContext.Set<T>().AsNoTracking();

    protected async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    protected async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class
    {
        await _dbContext.AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
    }

    protected async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    protected HttpClient GetAnonymousClient() => _fixture.AnonymousClient.Value;

    public ValueTask DisposeAsync()
    {
        _scope.Dispose();
        return ValueTask.CompletedTask;
    }
}