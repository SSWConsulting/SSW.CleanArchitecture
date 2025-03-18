using Microsoft.Extensions.DependencyInjection;
using WebApi.IntegrationTests.Common.Database;

namespace WebApi.IntegrationTests.Common;

public static class Testing
{
    private static readonly SqlServerTestDatabase _database = new();
    private static WebApiTestFactory _factory = null!;
    private static IServiceScopeFactory _scopeFactory = null!;

    [Before(Assembly)]
    public static async Task GlobalSetup()
    {
        await _database.InitializeAsync();
        _factory = new WebApiTestFactory(_database.DbConnection);
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    [BeforeEvery(Test)]
    public static async Task TestSetup(TestContext _)
    {
        // Nothing to do yet
        await Task.CompletedTask;
    }

    // NOTE: Could change this to class or assembly level if you want to reset the database less often
    [AfterEvery(Test)]
    public static async Task TestCleanUp(TestContext _)
    {
        await _database.ResetAsync();
    }

    [After(Assembly)]
    public static async Task GlobalCleanUp()
    {
        await _database.DisposeAsync();
        await _factory.DisposeAsync();
    }

    // NOTE: If you need an authenticated client, create a similar method that performance the authentication,
    // adds the appropriate headers and returns the authenticated client
    // For an example of this see https://github.com/SSWConsulting/Northwind365
    public static Lazy<HttpClient> AnonymousClient => new(_factory.CreateClient());

    public static IServiceScope CreateScope() => _scopeFactory.CreateScope();
}