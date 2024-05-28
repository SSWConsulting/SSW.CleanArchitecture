using Ardalis.Specification;

namespace SSW.CleanArchitecture.Domain.Heroes;

public sealed class HeroByIdSpec : SingleResultSpecification<Hero>
{
    public HeroByIdSpec(HeroId heroId)
    {
        Query.Where(t => t.Id == heroId)
            // TODO: Check this is needed and doesn't break anything
            .Include(t => t.Powers);
    }
}