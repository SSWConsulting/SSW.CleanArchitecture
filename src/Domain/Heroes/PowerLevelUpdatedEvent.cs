using SSW.CleanArchitecture.Domain.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Domain.Heroes;

public record PowerLevelUpdatedEvent : IDomainEvent
{
    public HeroId Id { get; }
    public TeamId? TeamId { get; }
    public string HeroName { get; }
    public int HeroPowerLevel { get; }

    public PowerLevelUpdatedEvent(Hero hero)
    {
        ThrowIfNull(hero);

        Id = hero.Id;
        TeamId = hero.TeamId;
        HeroName = hero.Name;
        HeroPowerLevel = hero.PowerLevel;
    }
}