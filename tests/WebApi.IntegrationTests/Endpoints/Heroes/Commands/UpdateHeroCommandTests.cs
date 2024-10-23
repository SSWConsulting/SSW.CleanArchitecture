using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.Features.Heroes.Commands.UpdateHero;
using SSW.CleanArchitecture.Domain.Heroes;
using System.Net;
using System.Net.Http.Json;
using WebApi.IntegrationTests.Common.Factories;
using WebApi.IntegrationTests.Common.Fixtures;

namespace WebApi.IntegrationTests.Endpoints.Heroes.Commands;

public class UpdateHeroCommandTests(TestingDatabaseFixture fixture, ITestOutputHelper output)
    : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task Command_ShouldUpdateHero()
    {
        // Arrange
        var heroName = "2021-01-01T00:00:00Z";
        var heroAlias = "2021-01-01T00:00:00Z-alias";
        var hero = HeroFactory.Generate();
        await AddEntityAsync(hero);
        (string Name, int PowerLevel)[] powers = [
            ("Heat vision", 7),
            ("Super-strength", 10),
            ("Flight", 8),
        ];
        var cmd = new UpdateHeroCommand(
            heroName,
            heroAlias,
            powers.Select(p => new UpdateHeroPowerDto { Name = p.Name, PowerLevel = p.PowerLevel }));
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
    
    [Fact]
    public async Task Command_WhenHeroDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var heroId = new HeroId(Guid.NewGuid());
        var cmd = new UpdateHeroCommand(
            "foo",
            "bar",
            new [] { new UpdateHeroPowerDto { Name = "Heat vision", PowerLevel = 7 } });
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