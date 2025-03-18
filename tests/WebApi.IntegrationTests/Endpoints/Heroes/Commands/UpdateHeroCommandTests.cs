using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.UseCases.Heroes.Commands.UpdateHero;
using SSW.CleanArchitecture.Domain.Heroes;
using System.Net;
using System.Net.Http.Json;
using WebApi.IntegrationTests.Common.Factories;
using WebApi.IntegrationTests.Common.Fixtures;

namespace WebApi.IntegrationTests.Endpoints.Heroes.Commands;

public class UpdateHeroCommandTests(TestingDatabaseFixture fixture, ITestOutputHelper output)
    : IntegrationTestBase(fixture, output)
{
    [Test]
    public async Task Command_ShouldUpdateHero()
    {
        // Arrange
        var heroName = "2021-01-01T00:00:00Z";
        var heroAlias = "2021-01-01T00:00:00Z-alias";
        var hero = HeroFactory.Generate();
        Context.Heroes.Add(hero);
        await Context.SaveChangesAsync();
        (string Name, int PowerLevel)[] powers =
        [
            ("Heat vision", 7),
            ("Super-strength", 10),
            ("Flight", 8),
        ];
        var cmd = new UpdateHeroCommand(
            heroName,
            heroAlias,
            powers.Select(p => new UpdateHeroPowerDto(p.Name, p.PowerLevel)));
        cmd.HeroId = hero.Id.Value;
        var client = GetAnonymousClient();
        var createdTimeStamp = DateTime.Now;

        // Act
        var result = await client.PutAsJsonAsync($"/api/heroes/{cmd.HeroId}", cmd);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        Hero item = await Context.Heroes.AsNoTracking().FirstAsync(dbHero => dbHero.Id == hero.Id);

        item.Should().NotBeNull();
        item.Name.Should().Be(cmd.Name);
        item.Alias.Should().Be(cmd.Alias);
        item.PowerLevel.Should().Be(25);
        item.Powers.Should().HaveCount(3);
        item.UpdatedAt.Should().NotBe(hero.CreatedAt);
        item.UpdatedAt.Should().BeCloseTo(createdTimeStamp, TimeSpan.FromSeconds(10));
    }

    [Test]
    public async Task Command_WhenHeroDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var heroId = HeroId.From(Guid.NewGuid());
        var cmd = new UpdateHeroCommand(
            "foo",
            "bar",
            [new UpdateHeroPowerDto("Heat vision", 7)]);
        cmd.HeroId = heroId.Value;
        var client = GetAnonymousClient();

        // Act
        var result = await client.PutAsJsonAsync("/heroes", cmd);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        Hero? item = await Context.Heroes.AsNoTracking().FirstOrDefaultAsync(dbHero => dbHero.Id == heroId);

        item.Should().BeNull();
    }
}