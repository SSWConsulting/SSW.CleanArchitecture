using Ardalis.GuardClauses;
using SSW.CleanArchitecture.Domain.Common.Base;

namespace SSW.CleanArchitecture.Domain.Heroes;

public record PowerLevelUpdatedEvent : DomainEvent
{
    public HeroId Id { get; }
    public string Name { get; }
    public int PowerLevel { get; }

    public PowerLevelUpdatedEvent(Hero hero)
    {
        Guard.Against.Null(hero);

        Id = hero.Id;
        Name = hero.Name;
        PowerLevel = hero.PowerLevel;
    }
}