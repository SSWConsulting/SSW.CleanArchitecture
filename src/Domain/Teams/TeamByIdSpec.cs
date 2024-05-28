using Ardalis.Specification;

namespace SSW.CleanArchitecture.Domain.Teams;

public sealed class TeamByIdSpec : SingleResultSpecification<Team>
{
    public TeamByIdSpec(TeamId teamId)
    {
        Query.Where(t => t.Id == teamId)
            .Include(t => t.Missions)
            .Include(t => t.Heroes);
    }
}