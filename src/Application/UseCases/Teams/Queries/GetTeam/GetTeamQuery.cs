using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.UseCases.Teams.Queries.GetTeam;

public record GetTeamQuery(Guid TeamId) : IRequest<ErrorOr<TeamDto>>;

public sealed class GetAllTeamsQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetTeamQuery, ErrorOr<TeamDto>>
{
    public async Task<ErrorOr<TeamDto>> Handle(
        GetTeamQuery request,
        CancellationToken cancellationToken)
    {
        var teamId = new TeamId(request.TeamId);

        var team = await dbContext.Teams
            .Where(t => t.Id == teamId)
            .Select(t => new TeamDto
            {
                Id = t.Id.Value,
                Name = t.Name,
                Heroes = t.Heroes.Select(h => new HeroDto { Id = h.Id.Value, Name = h.Name }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (team is null)
            return TeamErrors.NotFound;

        return team;
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