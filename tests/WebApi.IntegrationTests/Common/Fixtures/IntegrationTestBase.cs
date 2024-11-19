using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Instrumentation.Http;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Infrastructure.Persistence;
using TUnit.Core.Interfaces;

namespace WebApi.IntegrationTests.Common.Fixtures;

/// <summary>
/// Integration tests inherit from this to access helper classes
/// </summary>
[NotInParallel]
public abstract class IntegrationTestBaseV2 : IDisposable
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IServiceScope _scope;
    protected readonly ISender Mediator;

    public IntegrationTestBaseV2()
    {
        _scope = Testing.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        Mediator = _scope.ServiceProvider.GetRequiredService<ISender>();
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

    protected HttpClient GetAnonymousClient() => Testing.AnonymousClient.Value;

    public void Dispose()
    {
        _scope.Dispose();
    }
}