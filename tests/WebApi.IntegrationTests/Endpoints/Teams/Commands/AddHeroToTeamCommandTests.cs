using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Domain.Teams;
using System.Net;
using WebApi.IntegrationTests.Common;
using WebApi.IntegrationTests.Common.Factories;

namespace WebApi.IntegrationTests.Endpoints.Teams.Commands;

public class AddHeroToTeamCommandTests(TestingDatabaseFixture fixture) : IntegrationTestBase(fixture)
{
    [Fact]
    public async Task Command_ShouldAddHeroToTeam()
    {
        // Arrange
        var hero = HeroFactory.Generate();
        var team = TeamFactory.Generate();
        await AddAsync(team);
        await AddAsync(hero);

        var teamId = team.Id.Value;
        var heroId = hero.Id.Value;
        var client = GetAnonymousClient();

        // Act
        var result = await client.PostAsync($"/api/teams/{teamId}/heroes/{heroId}", null, CancellationToken);

        // Assert
        var updatedTeam = await GetQueryable<Team>()
            .WithSpecification(new TeamByIdSpec(team.Id))
            .FirstOrDefaultAsync(CancellationToken);

        result.StatusCode.Should().Be(HttpStatusCode.Created);
        updatedTeam.Should().NotBeNull();
        updatedTeam!.Heroes.Should().HaveCount(1);
        updatedTeam.Heroes.First().Id.Should().Be(hero.Id);
        updatedTeam.TotalPowerLevel.Should().Be(hero.PowerLevel);
    }
}