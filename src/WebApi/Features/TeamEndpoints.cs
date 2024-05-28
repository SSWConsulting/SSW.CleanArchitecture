using MediatR;
using SSW.CleanArchitecture.Application.Features.Teams.Commands.CreateTeam;
using SSW.CleanArchitecture.WebApi.Extensions;

namespace SSW.CleanArchitecture.WebApi.Features;

public static class TeamEndpoints
{
    public static void MapTeamEndpoints(this WebApplication app)
    {
        var group = app.MapApiGroup("teams");

        group
            .MapPost("/", async (ISender sender, CreateTeamCommand command, CancellationToken ct) =>
            {
                await sender.Send(command, ct);
                return Results.Created();
            })
            .WithName("CreateHero")
            .ProducesPost();
    }
}