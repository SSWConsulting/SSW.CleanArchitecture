using SSW.CleanArchitecture.Application.UseCases.Teams.Queries.GetAllTeams;
using System.Net.Http.Json;
using WebApi.IntegrationTests.Common;
using WebApi.IntegrationTests.Common.Factories;

namespace WebApi.IntegrationTests.Endpoints.Teams.Queries;

public class GetAllTeamsQueryTests : IntegrationTestBase
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