using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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
            .ProducesProblem();

        // TODO: Investigate examples for swagger docs. i.e. better docs than:
        // myWeirdField: "string" vs myWeirdField: "this-silly-string"
        // (https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/79)

        // TODO: PUT should take Hero ID in the URL, not in the body
        group
            .MapPut("/", async Task<Results<NotFound, NoContent>> (
                ISender sender,
                UpdateHeroCommand command,
                CancellationToken ct) =>
            {
                var result = await sender.Send(command, ct);
                if (result.IsNotFound())
                    return TypedResults.NotFound();

                return TypedResults.NoContent();
            })
            .WithName("UpdateHero")
            .ProducesProblem();

        group
            .MapPost("/", async (ISender sender, CreateHeroCommand command, CancellationToken ct) =>
            {
                await sender.Send(command, ct);
                return TypedResults.Created();
            })
            .WithName("CreateHero")
            .ProducesProblem();
    }
}