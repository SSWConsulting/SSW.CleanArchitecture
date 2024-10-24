using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Infrastructure.Persistence;

namespace WebApi.IntegrationTests.Common.Fixtures;

/// <summary>
/// Integration tests inherit from this to access helper classes
/// </summary>
[Collection(TestingDatabaseFixtureCollection.Name)]
public abstract class IntegrationTestBase : IAsyncLifetime
{
    private readonly IServiceScope _scope;

    private readonly TestingDatabaseFixture _fixture;
    protected IMediator Mediator { get; }

    // TODO: Consider removing this as query results can be cached and cause bad test results
    //       Also, consider encapsulating this and only exposing a `Query` method that internally uses `AsNoTracking()`
    //       see: https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/324
    public IApplicationDbContext Context => _dbContext;

    private readonly ApplicationDbContext _dbContext;

    protected IQueryable<T> GetQueryable<T>() where T : class => _dbContext.Set<T>().AsNoTracking();

    protected IntegrationTestBase(TestingDatabaseFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _fixture.Factory.Output = output;

        _scope = _fixture.ScopeFactory.CreateScope();
        Mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    public async Task InitializeAsync()
    {
        await _fixture.ResetState();
    }

    protected async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await Context.SaveChangesAsync(cancellationToken);
    }

    protected HttpClient GetAnonymousClient() => _fixture.Factory.AnonymousClient.Value;

    public Task DisposeAsync()
    {
        _scope.Dispose();
        return Task.CompletedTask;
    }
}