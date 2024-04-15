using Ardalis.GuardClauses;
using SSW.CleanArchitecture.Domain.Common;
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
        Name = Guard.Against.StringTooLong(name, Constants.DefaultNameMaxLength);
        PowerLevel = Guard.Against.OutOfRange(powerLevel, nameof(PowerLevel), 1, 10);
    }
}