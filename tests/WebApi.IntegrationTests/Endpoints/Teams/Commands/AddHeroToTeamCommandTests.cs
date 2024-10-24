using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Domain.Teams;
using System.Net;
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
        Context.Heroes.Add(hero);
        Context.Teams.Add(team);
        await Context.SaveChangesAsync();

        var teamId = team.Id.Value;
        var heroId = hero.Id.Value;
        var client = GetAnonymousClient();

        // Act
        var result = await client.PostAsync($"/api/teams/{teamId}/heroes/{heroId}", null);

        // Assert
        var updatedTeam = await Context.Teams
            .WithSpecification(new TeamByIdSpec(team.Id))
            .FirstOrDefaultAsync();

        // await Task.Delay(100000);

        result.StatusCode.Should().Be(HttpStatusCode.Created);
        updatedTeam.Should().NotBeNull();
        updatedTeam!.Heroes.Should().HaveCount(1);
        updatedTeam.Heroes.First().Id.Should().Be(hero.Id);
        updatedTeam.TotalPowerLevel.Should().Be(hero.PowerLevel);
    }
}