using SSW.CleanArchitecture.Application.Common.Interfaces;

namespace SSW.CleanArchitecture.Application.UseCases.Heroes.Queries.GetAllHeroes;

public record GetAllHeroesQuery : IRequest<IReadOnlyList<HeroDto>>;

public record HeroDto(Guid Id, string Name, string Alias, int PowerLevel, IEnumerable<HeroPowerDto> Powers);

public record HeroPowerDto(string Name, int PowerLevel);

internal sealed class GetAllHeroesQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetAllHeroesQuery, IReadOnlyList<HeroDto>>
{
    public async Task<IReadOnlyList<HeroDto>> Handle(
        GetAllHeroesQuery request,
        CancellationToken cancellationToken)
    {
        return await dbContext.Heroes
            .Select(h => new HeroDto(
                h.Id.Value,
                h.Name,
                h.Alias,
                h.PowerLevel,
                h.Powers.Select(p => new HeroPowerDto(p.Name, p.PowerLevel))))
            .ToListAsync(cancellationToken);
    }
}