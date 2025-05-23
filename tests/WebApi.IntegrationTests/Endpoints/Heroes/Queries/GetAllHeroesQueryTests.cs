using SSW.CleanArchitecture.Application.UseCases.Heroes.Queries.GetAllHeroes;
using System.Net.Http.Json;
using WebApi.IntegrationTests.Common;
using WebApi.IntegrationTests.Common.Factories;

namespace WebApi.IntegrationTests.Endpoints.Heroes.Queries;

public class GetAllHeroesQueryTests(TestingDatabaseFixture fixture) : IntegrationTestBase(fixture)
{
    [Fact]
    public async Task Query_ShouldReturnAllHeroes()
    {
        // Arrange
        const int entityCount = 10;
        var entities = HeroFactory.Generate(entityCount);
        await AddRangeAsync(entities);
        var client = GetAnonymousClient();

        // Act
        var result = await client.GetFromJsonAsync<HeroDto[]>("/api/heroes", CancellationToken);

        // Assert
        result.Should().NotBeNull();
        result!.Length.Should().Be(entityCount);
    }
}