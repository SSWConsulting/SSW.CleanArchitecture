using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Domain.Teams;
using SSW.CleanArchitecture.WebApi.Features;
using System.Net;
using System.Net.Http.Json;
using WebApi.IntegrationTests.Common.Factories;
using WebApi.IntegrationTests.Common.Fixtures;

namespace WebApi.IntegrationTests.Endpoints.Teams.Commands;

public class ExecuteMissionCommandTests(TestingDatabaseFixture fixture, ITestOutputHelper output)
    : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task Command_ShouldExecuteMission()
    {
        // Arrange
        var hero = HeroFactory.Generate();
        var team = TeamFactory.Generate();
        await AddEntityAsync(hero);
        await AddEntityAsync(team);
        team.AddHero(hero);
        await Context.SaveChangesAsync();
        var teamId = team.Id.Value;
        var client = GetAnonymousClient();
        var request = new ExcuteMissionRequest("Save the world");

        // Act
        var result = await client.PostAsJsonAsync($"/api/teams/{teamId}/missions", request);

        // Assert
        var response = await result.Content.ReadFromJsonAsync<Guid>();
        var missionId = new MissionId(response);
        var mission = await GetQueryable<Mission>().FirstOrDefaultAsync(m => m.Id == missionId);

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        mission.Should().NotBeNull();
        mission!.Description.Should().Be(request.Description);
    }
}