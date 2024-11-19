using SSW.CleanArchitecture.Application.UseCases.Teams.Queries.GetAllTeams;
using System.Net.Http.Json;
using WebApi.IntegrationTests.Common.Factories;
using WebApi.IntegrationTests.Common.Fixtures;

namespace WebApi.IntegrationTests.Endpoints.Teams.Queries;

public class GetAllTeamsQueryTests : IntegrationTestBaseV2
{
    [Test]
    public async Task Query_ShouldReturnAllTeams()
    {
        // Arrange
        const int entityCount = 10;
        var entities = TeamFactory.Generate(entityCount);
        await AddRangeAsync(entities);
        // await Context.SaveChangesAsync();
        var client = GetAnonymousClient();

        // Act
        var result = await client.GetFromJsonAsync<TeamDto[]>("/api/teams");

        // Assert
        result.Should().NotBeNull();
        result!.Length.Should().Be(entityCount);

        var firstTeam = result.First();
        firstTeam.Id.Should().NotBeEmpty();
        firstTeam.Name.Should().NotBeEmpty();
    }
}