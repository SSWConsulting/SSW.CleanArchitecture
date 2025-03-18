using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SSW.CleanArchitecture.Infrastructure.Persistence;

namespace WebApi.IntegrationTests.Common.Fixtures;

/// <summary>
/// Integration tests inherit from this to access helper classes
/// </summary>
[NotInParallel]
public abstract class IntegrationTestBaseV2 : IDisposable
{
    private ApplicationDbContext _dbContext = null!;
    private IServiceScope _scope = null!;

    [Before(Test)]
    public void TestSetup()
    {
        _scope = Testing.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
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

    protected HttpClient GetAnonymousClient() => Testing.AnonymousClient.Value;

    public void Dispose()
    {
        _scope.Dispose();
    }
}