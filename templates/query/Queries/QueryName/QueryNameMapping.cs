using SSW.CleanArchitecture.Domain.Entities;

namespace SSW.CleanArchitecture.Application.UseCases.EntityNames.Queries.QueryName;

public class QueryNameMapping : Profile
{
    public QueryNameMapping()
    {
        CreateMap<EntityName, EntityNameDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
    }
}
