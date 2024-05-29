using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Application.Features.Teams.Queries.GetAllTeams;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.Features.Teams.Queries.GetTeam;

public record GetTeamQuery(Guid TeamId) : IRequest<IReadOnlyList<TeamDto>>;

public sealed class GetAllTeamsQueryHandler(
    IMapper mapper,
    IApplicationDbContext dbContext) : IRequestHandler<GetTeamQuery, IReadOnlyList<TeamDto>>
{
    public async Task<IReadOnlyList<TeamDto>> Handle(
        GetTeamQuery request,
        CancellationToken cancellationToken)
    {
        var teamId = new TeamId(request.TeamId);

        return await dbContext.Teams
            .Where(t => t.Id == teamId)
            .Select(t => new TeamDto
            {
                Id = t.Id.Value,
                Name = t.Name,
                Heroes = t.Heroes.Select(h => new HeroDto
                {
                    Id = h.Id.Value,
                    Name = h.Name
                }).ToList()

            })
            //.ProjectTo<TeamDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

public class TeamDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public List<HeroDto> Heroes { get; init; } = [];
}

public class HeroDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
}

// public class GetTeamMapping : Profile
// {
//     public GetTeamMapping()
//     {
//         CreateMap<Team, TeamDto>()
//             .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
//     }
// }