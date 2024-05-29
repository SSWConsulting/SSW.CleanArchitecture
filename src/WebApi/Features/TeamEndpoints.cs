using MediatR;
using Microsoft.AspNetCore.Mvc;
using SSW.CleanArchitecture.Application.Features.Teams.Commands.AddHeroToTeam;
using SSW.CleanArchitecture.Application.Features.Teams.Commands.CompleteMission;
using SSW.CleanArchitecture.Application.Features.Teams.Commands.CreateTeam;
using SSW.CleanArchitecture.Application.Features.Teams.Commands.ExecuteMission;
using SSW.CleanArchitecture.Application.Features.Teams.Queries.GetAllTeams;
using SSW.CleanArchitecture.Application.Features.Teams.Queries.GetTeam;
using SSW.CleanArchitecture.Domain.Heroes;
using SSW.CleanArchitecture.Domain.Teams;
using SSW.CleanArchitecture.WebApi.Extensions;
using TeamDto = SSW.CleanArchitecture.Application.Features.Teams.Queries.GetAllTeams.TeamDto;

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
            .WithName("CreateTeam")
            .ProducesPost();

        group
            .MapGet("/", async (ISender sender, CancellationToken ct) =>
            {
                var results = await sender.Send(new GetAllTeamsQuery(), ct);
                return Results.Ok(results);
            })
            .WithName("GetAllTeams")
            .ProducesGet<TeamDto[]>();

        group
            .MapPost("/{teamId:guid}/heroes/{heroId:guid}",
                async (ISender sender, Guid teamId, Guid heroId, CancellationToken ct) =>
                {
                    var command = new AddHeroToTeamCommand(teamId, heroId);
                    await sender.Send(command, ct);
                    return Results.Created();
                })
            .WithName("AddHeroToTeam")
            .ProducesPost();

        group
            .MapGet("/{teamId:guid}",
                async (ISender sender, Guid teamId, CancellationToken ct) =>
                {
                    var query = new GetTeamQuery(teamId);
                    var results = await sender.Send(query, ct);
                    return Results.Ok(results);
                })
            .WithName("GetTeam")
            .ProducesGet<TeamDto>();

        group
            .MapPost("/{teamId:guid}/execute-mission",
                async (ISender sender, Guid teamId, [FromBody] ExcuteMissionRequest request, CancellationToken ct) =>
                {
                    var command = new ExecuteMissionCommand(teamId, request.Description);
                    var response = await sender.Send(command, ct);
                    return Results.Ok(response);
                })
            .WithName("ExecuteMission")
            .ProducesPost();

        group
            .MapPost("/{teamId:guid}/complete-mission",
                async (ISender sender, Guid teamId, CancellationToken ct) =>
                {
                    var command = new CompleteMissionCommand(teamId);
                    await sender.Send(command, ct);
                    return Results.Ok();
                })
            .WithName("CompleteMission")
            .ProducesPost();
    }
}

public record ExcuteMissionRequest(string Description);