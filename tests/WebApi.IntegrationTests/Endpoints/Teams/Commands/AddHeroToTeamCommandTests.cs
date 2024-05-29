using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.Features.Teams.Commands.AddHeroToTeam;
using SSW.CleanArchitecture.Application.Features.Teams.Commands.CreateTeam;
using SSW.CleanArchitecture.Domain.Teams;
using System.Net;
using System.Net.Http.Json;
using WebApi.IntegrationTests.Common.Factories;
using WebApi.IntegrationTests.Common.Fixtures;

namespace WebApi.IntegrationTests.Endpoints.Teams.Commands;

public class AddHeroToTeamCommandTests(TestingDatabaseFixture fixture, ITestOutputHelper output)
    : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task Command_ShouldAddHeroToTeam()
    {
        // Arrange
        var hero = HeroFactory.Generate();
        var team = TeamFactory.Generate();
        await AddEntityAsync(hero);
        await AddEntityAsync(team);
        var teamId = team.Id.Value;
        var heroId = hero.Id.Value;
        // var cmd = new AddHeroToTeamCommand(team.Id.Value, hero.Id.Value);
        var client = GetAnonymousClient();

        // Act
        var result = await client.PostAsync($"/api/teams/{teamId}/hero/{heroId}", null);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        var updatedTeam = await Context.Teams
            .Include(t => t.Heroes)
            // .WithSpecification(new TeamByIdSpec(new TeamId(teamId)))
            .FirstOrDefaultAsync(t => t.Id == team.Id);

        updatedTeam.Should().NotBeNull();
        // TODO: Test this
        updatedTeam!.Heroes.Should().Contain(hero);
        // updatedTeam.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(10));
    }
}