using SSW.CleanArchitecture.Application.UseCases.Heroes.Queries.GetAllHeroes;
using System.Net.Http.Json;
using WebApi.IntegrationTests.Common.Factories;
using WebApi.IntegrationTests.Common.Fixtures;

namespace WebApi.IntegrationTests.Endpoints.Heroes.Queries;

public class GetAllHeroesQueryTests(TestingDatabaseFixture fixture, ITestOutputHelper output)
    : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task Query_ShouldReturnAllHeroes()
    {
        // Arrange
        const int entityCount = 10;
        var entities = HeroFactory.Generate(entityCount);
        await Context.Heroes.AddRangeAsync(entities);
        await Context.SaveChangesAsync();
        var client = GetAnonymousClient();

        // Act
        var result = await client.GetFromJsonAsync<HeroDto[]>("/api/heroes");

        // Assert
        result.Should().NotBeNull();
        result!.Length.Should().Be(entityCount);
    }
}