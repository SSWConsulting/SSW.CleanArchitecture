using SSW.CleanArchitecture.Domain.Heroes;

namespace SSW.CleanArchitecture.Application.UseCases.Heroes.Queries.GetAllHeroes;

public class GetAllHeroesMapping : Profile
{
    public GetAllHeroesMapping()
    {
        CreateMap<Hero, HeroDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));

        CreateMap<Power, HeroPowerDto>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.PowerLevel, opt => opt.MapFrom(s => s.PowerLevel));
    }
}