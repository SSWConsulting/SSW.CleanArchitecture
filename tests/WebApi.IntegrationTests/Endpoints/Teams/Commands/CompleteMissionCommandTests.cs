using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Domain.Teams;
using System.Net;
using WebApi.IntegrationTests.Common.Factories;
using WebApi.IntegrationTests.Common.Fixtures;

namespace WebApi.IntegrationTests.Endpoints.Teams.Commands;

public class CompleteMissionCommandTests : IntegrationTestBaseV2
{
    [Test]
    public async Task Command_ShouldCompleteMission()
    {
        // Arrange
        var hero = HeroFactory.Generate();
        var team = TeamFactory.Generate();
        team.AddHero(hero);
        team.ExecuteMission("Save the world");
        await AddAsync(team);
        // await Context.SaveChangesAsync();
        var teamId = team.Id.Value;
        var client = GetAnonymousClient();

        // Act
        var result = await client.PostAsync($"/api/teams/{teamId}/complete-mission", null);

        // Assert
        var updatedTeam = await GetQueryable<Team>()
            .WithSpecification(new TeamByIdSpec(team.Id))
            .FirstOrDefaultAsync();
        var mission = updatedTeam!.Missions.First();

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        updatedTeam!.Missions.Should().HaveCount(1);
        updatedTeam.Status.Should().Be(TeamStatus.Available);
        mission.Status.Should().Be(MissionStatus.Complete);
    }
}