using SSW.CleanArchitecture.Domain.Common.Interfaces;

namespace SSW.CleanArchitecture.Domain.Heroes;

public record Power : IValueObject
{
    // Private setters needed for EF
    public string Name { get; private set; }

    // Private setters needed for EF
    public int PowerLevel { get; private set; }

    public Power(string name, int powerLevel)
    {
        ThrowIfLessThan(powerLevel, 1);
        ThrowIfGreaterThan(powerLevel, 10);
        Name = name;
        PowerLevel = powerLevel;
    }
}