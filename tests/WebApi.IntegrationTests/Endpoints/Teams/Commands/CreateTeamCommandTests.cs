using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.UseCases.Teams.Commands.CreateTeam;
using SSW.CleanArchitecture.Domain.Teams;
using System.Net;
using System.Net.Http.Json;
using WebApi.IntegrationTests.Common;

namespace WebApi.IntegrationTests.Endpoints.Teams.Commands;

public class CreateTeamCommandTests : IntegrationTestBase
{
    [Test]
    public async Task Command_ShouldCreateTeam()
    {
        // Arrange
        var cmd = new CreateTeamCommand("Clark Kent");
        var client = GetAnonymousClient();

        // Act
        var result = await client.PostAsJsonAsync("/api/teams", cmd, CancellationToken);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        var item = await GetQueryable<Team>().FirstAsync(CancellationToken);

        item.Should().NotBeNull();
        item.Name.Should().Be(cmd.Name);
        item.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(10));
    }
}