using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.Features.Heroes.Commands.CreateHero;
using SSW.CleanArchitecture.Domain.Teams;
using System.Net;
using System.Net.Http.Json;
using WebApi.IntegrationTests.Common.Fixtures;

namespace WebApi.IntegrationTests.Endpoints.Heroes.Commands.CreateHero;

public class CreateHeroCommandTests(TestingDatabaseFixture fixture, ITestOutputHelper output)
    : IntegrationTestBase(fixture, output), IAsyncLifetime
{
    private TeamId _teamId;
    
    public new async Task InitializeAsync()
    {
        await base.InitializeAsync();

        var team = Team.Create("TeamSuper");
        Context.Teams.Add(team);
        await Context.SaveChangesAsync();
        
        _teamId = team.Id;
    }
    
    [Fact]
    public async Task Command_ShouldCreateHero()
    {
        // Arrange
        (string Name, int PowerLevel)[] powers = [
            ("Heat vision", 7),
            ("Super-strength", 10),
            ("Flight", 8),
        ];
        var cmd = new CreateHeroCommand(
            "Clark Kent", 
            "Superman",
            _teamId.Value,
            powers.Select(p => new CreateHeroPowerDto { Name = p.Name, PowerLevel = p.PowerLevel }));
        var client = GetAnonymousClient();

        // Act
        var result = await client.PostAsJsonAsync("/heroes", cmd);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        var item = await Context.Heroes.FirstAsync();

        item.Should().NotBeNull();
        item.Name.Should().Be(cmd.Name);
        item.Alias.Should().Be(cmd.Alias);
        item.PowerLevel.Should().Be(25);
        item.Powers.Should().HaveCount(3);
        item.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(10));
    }
}