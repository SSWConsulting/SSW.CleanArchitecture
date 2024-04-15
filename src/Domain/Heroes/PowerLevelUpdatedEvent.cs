using Ardalis.GuardClauses;
using SSW.CleanArchitecture.Domain.Common.Base;

namespace SSW.CleanArchitecture.Domain.Heroes;

public record PowerLevelUpdatedEvent : DomainEvent
{
    public HeroId Id { get; }
    public string Name { get; }
    public int PowerDifference { get; }

    public PowerLevelUpdatedEvent(HeroId id, string name, int oldPowerLevel, int newPowerLevel)
    {
        Guard.Against.Null(id);
        Guard.Against.NullOrWhiteSpace(name);

        Id = id;
        Name = name;
        PowerDifference = newPowerLevel - oldPowerLevel;
    }
}