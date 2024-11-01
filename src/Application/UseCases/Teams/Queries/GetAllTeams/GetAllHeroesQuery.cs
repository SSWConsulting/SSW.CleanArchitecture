using AutoMapper.QueryableExtensions;
using SSW.CleanArchitecture.Application.Common.Interfaces;

namespace SSW.CleanArchitecture.Application.UseCases.Teams.Queries.GetAllTeams;

public record GetAllTeamsQuery : IRequest<IReadOnlyList<TeamDto>>;

public sealed class GetAllTeamsQueryHandler(
    IMapper mapper,
    IApplicationDbContext dbContext) : IRequestHandler<GetAllTeamsQuery, IReadOnlyList<TeamDto>>
{
    public async Task<IReadOnlyList<TeamDto>> Handle(
        GetAllTeamsQuery request,
        CancellationToken cancellationToken)
    {
        return await dbContext.Teams
            .ProjectTo<TeamDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}