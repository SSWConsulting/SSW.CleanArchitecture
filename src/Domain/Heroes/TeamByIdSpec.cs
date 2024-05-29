using Ardalis.Specification;

namespace SSW.CleanArchitecture.Domain.Heroes;

public sealed class HeroByIdSpec : SingleResultSpecification<Hero>
{
    public HeroByIdSpec(HeroId heroId)
    {
        Query.Where(t => t.Id == heroId);
    }
}