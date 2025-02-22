using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.UseCases.Teams.Commands.CreateTeam;
using System.Net;
using System.Net.Http.Json;
using WebApi.IntegrationTests.Common.Fixtures;

namespace WebApi.IntegrationTests.Endpoints.Teams.Commands;

public class CreateTeamCommandTests(TestingDatabaseFixture fixture, ITestOutputHelper output)
    : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task Command_ShouldCreateTeam()
    {
        // Arrange
        var cmd = new CreateTeamCommand("Clark Kent");
        var client = GetAnonymousClient();

        // Act
        var result = await client.PostAsJsonAsync("/api/teams", cmd);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        var item = await Context.Teams.AsNoTracking().FirstAsync();

        item.Should().NotBeNull();
        item.Name.Should().Be(cmd.Name);
        item.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(10));
    }
}