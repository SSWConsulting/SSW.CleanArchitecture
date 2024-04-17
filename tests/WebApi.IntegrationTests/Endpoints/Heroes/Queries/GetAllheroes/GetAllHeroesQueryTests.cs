using SSW.CleanArchitecture.Application.Features.Heroes.Queries.GetAllHeroes;
using System.Net.Http.Json;
using WebApi.IntegrationTests.Common.Factories;
using WebApi.IntegrationTests.Common.Fixtures;

namespace WebApi.IntegrationTests.Endpoints.Heroes.Queries.GetAllheroes;

public class GetAllHeroesQueryTests(TestingDatabaseFixture fixture, ITestOutputHelper output)
    : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task Query_ShouldReturnAllTodoItems()
    {
        // Arrange
        const int entityCount = 10;
        var entities = HeroFactory.Generate(entityCount);
        await AddEntitiesAsync(entities);
        var client = GetAnonymousClient();

        // Act
        var result = await client.GetFromJsonAsync<HeroDto[]>("/heroes");

        // Assert
        result.Should().NotBeNull();
        result!.Length.Should().Be(entityCount);
    }
}