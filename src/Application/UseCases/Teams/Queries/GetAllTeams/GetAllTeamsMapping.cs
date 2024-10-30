using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.UseCases.Teams.Queries.GetAllTeams;

public class GetAllTeamsMapping : Profile
{
    public GetAllTeamsMapping()
    {
        CreateMap<Team, TeamDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
    }
}