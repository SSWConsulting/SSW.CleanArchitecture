using Ardalis.GuardClauses;
using SSW.CleanArchitecture.Domain.Common.Base;

namespace SSW.CleanArchitecture.Domain.Heroes;

public record PowerLevelUpdatedEvent : DomainEvent
{
    public HeroId Id { get; }
    public string Name { get; }
    public int OldPowerLevel { get; }
    public int NewPowerLevel { get; }

    public PowerLevelUpdatedEvent(HeroId id, string name, int oldPowerLevel, int newPowerLevel)
    {
        Guard.Against.Null(id);
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.Null(oldPowerLevel);
        Guard.Against.Null(newPowerLevel);
        Guard.Against.Negative(newPowerLevel);
        Guard.Against.Negative(oldPowerLevel);

        Id = id;
        Name = name;
        OldPowerLevel = oldPowerLevel;
        NewPowerLevel = newPowerLevel;
    }
}