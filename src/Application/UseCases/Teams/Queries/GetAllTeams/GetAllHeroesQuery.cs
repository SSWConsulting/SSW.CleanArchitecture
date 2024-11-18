using SSW.CleanArchitecture.Application.Common.Interfaces;

namespace SSW.CleanArchitecture.Application.UseCases.Teams.Queries.GetAllTeams;

public record GetAllTeamsQuery : IRequest<IReadOnlyList<TeamDto>>;

public record TeamDto(Guid Id, string Name);

internal sealed class GetAllTeamsQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetAllTeamsQuery, IReadOnlyList<TeamDto>>
{
    public async Task<IReadOnlyList<TeamDto>> Handle(
        GetAllTeamsQuery request,
        CancellationToken cancellationToken)
    {
        return await dbContext.Teams
            .Select(t => new TeamDto(t.Id.Value, t.Name))
            .ToListAsync(cancellationToken);
    }
}