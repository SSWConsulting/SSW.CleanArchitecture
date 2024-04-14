using Ardalis.GuardClauses;
using SSW.CleanArchitecture.Domain.Common.Base;

namespace SSW.CleanArchitecture.Domain.Heroes;

public record StrengthUpdatedEvent : DomainEvent
{
    public HeroId Id { get; }
    public string Name { get; }
    public int Strength { get; }

    public StrengthUpdatedEvent(Hero hero)
    {
        Guard.Against.Null(hero);

        Id = hero.Id;
        Name = hero.Name;
        Strength = hero.Strength;
    }
}