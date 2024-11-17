using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Infrastructure.Persistence;
using TUnit.Core.Interfaces;

namespace WebApi.IntegrationTests.Common.Fixtures;

/// <summary>
/// Integration tests inherit from this to access helper classes
/// </summary>
// [Collection(TestingDatabaseFixtureCollection.Name)]
public abstract class IntegrationTestBase : IAsyncInitializer
{
    private IServiceScope _scope;

    private WebApplicationFactory _fixture;
    protected IMediator Mediator { get; private set; }

    // TODO: Consider removing this as query results can be cached and cause bad test results
    //       Also, consider encapsulating this and only exposing a `Query` method that internally uses `AsNoTracking()`
    //       see: https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/324
    public IApplicationDbContext Context => _dbContext;

    private ApplicationDbContext _dbContext;

    protected IQueryable<T> GetQueryable<T>() where T : class => _dbContext.Set<T>().AsNoTracking();

    protected IntegrationTestBase(WebApplicationFactory fixture)
    {
        _fixture = fixture;
    }

    public async Task InitializeAsync()
    {
        _scope = _fixture.ScopeFactory.CreateScope();
        Mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await _fixture.ResetState();
    }

    protected async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await Context.SaveChangesAsync(cancellationToken);
    }

    protected HttpClient GetAnonymousClient() => _fixture.AnonymousClient.Value;

    public Task DisposeAsync()
    {
        _scope.Dispose();
        return Task.CompletedTask;
    }
}