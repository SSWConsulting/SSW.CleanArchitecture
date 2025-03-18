using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.UseCases.Heroes.Commands.UpdateHero;
using SSW.CleanArchitecture.Domain.Heroes;
using SSW.CleanArchitecture.Domain.Teams;
using System.Net;
using System.Net.Http.Json;
using WebApi.IntegrationTests.Common;
using WebApi.IntegrationTests.Common.Factories;
using WebApi.IntegrationTests.Common.Fixtures;

namespace WebApi.IntegrationTests.Endpoints.Teams.Events;

public class UpdatePowerLevelEventTests : IntegrationTestBaseV2
{
    [Test]
    public async Task Command_UpdatePowerOnTeam()
    {
        // Arrange
        var hero = HeroFactory.Generate();
        var team = TeamFactory.Generate();
        List<Power> powers = [new Power("Strength", 10)];
        hero.UpdatePowers(powers);
        team.AddHero(hero);
        await AddAsync(team);
        // await Context.SaveChangesAsync();
        powers.Add(new Power("Speed", 5));
        var powerDtos = powers.Select(p => new UpdateHeroPowerDto(p.Name, p.PowerLevel));
        var cmd = new UpdateHeroCommand(hero.Name, hero.Alias, powerDtos);
        cmd.HeroId = hero.Id.Value;
        var client = GetAnonymousClient();

        // Act
        var result = await client.PutAsJsonAsync($"/api/heroes/{cmd.HeroId}", cmd);

        // Assert
        await Wait.ForEventualConsistency();
        var updatedTeam = await GetQueryable<Team>()
            .WithSpecification(new TeamByIdSpec(team.Id))
            .FirstOrDefaultAsync();

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        updatedTeam!.TotalPowerLevel.Should().Be(15);
    }
}