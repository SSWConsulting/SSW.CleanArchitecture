using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.UseCases.Heroes.Commands.CreateHero;
using SSW.CleanArchitecture.Domain.Heroes;
using System.Net;
using System.Net.Http.Json;
using WebApi.IntegrationTests.Common.Fixtures;

namespace WebApi.IntegrationTests.Endpoints.Heroes.Commands;

public class CreateHeroCommandTests : IntegrationTestBaseV2
{
    [Test]
    public async Task Command_ShouldCreateHero()
    {
        // Arrange
        (string Name, int PowerLevel)[] powers =
        [
            ("Heat vision", 7),
            ("Super-strength", 10),
            ("Flight", 8),
        ];
        var cmd = new CreateHeroCommand(
            "Clark Kent",
            "Superman",
            powers.Select(p => new CreateHeroPowerDto(p.Name, p.PowerLevel)));
        var client = GetAnonymousClient();

        // Act
        var result = await client.PostAsJsonAsync("/api/heroes", cmd);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        var item = await GetQueryable<Hero>().FirstAsync();

        item.Should().NotBeNull();
        item.Name.Should().Be(cmd.Name);
        item.Alias.Should().Be(cmd.Alias);
        item.PowerLevel.Should().Be(25);
        item.Powers.Should().HaveCount(3);
        item.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(10));
    }
}