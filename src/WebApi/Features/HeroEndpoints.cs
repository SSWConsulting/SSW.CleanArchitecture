using MediatR;
using SSW.CleanArchitecture.Application.Features.Heroes.Commands.CreateHero;
using SSW.CleanArchitecture.Application.Features.Heroes.Commands.UpdateHero;
using SSW.CleanArchitecture.Application.Features.Heroes.Queries.GetAllHeroes;
using SSW.CleanArchitecture.WebApi.Extensions;

namespace SSW.CleanArchitecture.WebApi.Features;

public static class HeroEndpoints
{
    public static void MapHeroEndpoints(this WebApplication app)
    {
        var group = app.MapApiGroup("heroes");

        group
            .MapGet("/", async (ISender sender, CancellationToken ct) =>
            {
                var results = await sender.Send(new GetAllHeroesQuery(), ct);
                return TypedResults.Ok(results);
            })
            .WithName("GetAllHeroes")
            .ProducesGet<HeroDto[]>();

        group
            .MapPut("/{heroId:guid}", async (
                Guid heroId,
                UpdateHeroCommand command,
                ISender sender,
                CancellationToken ct) =>
            {
                command.HeroId = heroId;
                var result = await sender.Send(command, ct);
                return result.Match(_ => TypedResults.NoContent(), CustomResult.Problem);
            })
            .WithName("UpdateHero")
            .ProducesPut();

        group
            .MapPost("/", async (ISender sender, CreateHeroCommand command, CancellationToken ct) =>
            {
                var result = await sender.Send(command, ct);
                return result.Match(_ => TypedResults.Created(), CustomResult.Problem);
            })
            .WithName("CreateHero")
            .ProducesPost();
    }
}