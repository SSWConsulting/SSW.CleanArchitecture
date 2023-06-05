using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Application.IntegrationTests.TestHelpers;

namespace SSW.CleanArchitecture.Application.IntegrationTests;

[Collection(TestingDatabaseFixture.DatabaseCollectionDefinition)]
public abstract class IntegrationTestBase : IAsyncLifetime
{
    private readonly IServiceScope _scope;

    protected TestingDatabaseFixture Fixture { get; }
    protected IMediator Mediator { get; }
    protected IApplicationDbContext Context { get; }


    public IntegrationTestBase(TestingDatabaseFixture fixture)
    {
        Fixture = fixture;

        _scope = Fixture.ScopeFactory.CreateScope();
        Mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        Context = _scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
    }

    public async Task InitializeAsync()
    {
        await Fixture.ResetState();
    }

    public Task DisposeAsync()
    {
        _scope.Dispose();
        return Task.CompletedTask;
    }
}