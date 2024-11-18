using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.UseCases.Teams.Queries.GetTeam;

public record GetTeamQuery(Guid TeamId) : IRequest<ErrorOr<TeamDto>>;

public record TeamDto(Guid Id, string Name, IEnumerable<HeroDto> Heroes);

public record HeroDto(Guid Id, string Name);

internal sealed class GetAllTeamsQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetTeamQuery, ErrorOr<TeamDto>>
{
    public async Task<ErrorOr<TeamDto>> Handle(
        GetTeamQuery request,
        CancellationToken cancellationToken)
    {
        var teamId = TeamId.From(request.TeamId);

        var team = await dbContext.Teams
            .Where(t => t.Id == teamId)
            .Select(t => new TeamDto(
                t.Id.Value,
                t.Name,
                t.Heroes.Select(h => new HeroDto(h.Id.Value, h.Name))))
            .FirstOrDefaultAsync(cancellationToken);

        if (team is null)
            return TeamErrors.NotFound;

        return team;
    }
}