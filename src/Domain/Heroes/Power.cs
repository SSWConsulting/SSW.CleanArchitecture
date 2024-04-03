using Ardalis.GuardClauses;
using SSW.CleanArchitecture.Domain.Common;
using SSW.CleanArchitecture.Domain.Common.Interfaces;

namespace SSW.CleanArchitecture.Domain.Heroes;

public record Power : IValueObject
{
    // Private setters needed for EF
    public string Name { get; private set; }

    // Private setters needed for EF
    public int Strength { get; private set; }

    public Power(string name, int strength)
    {
        Name = Guard.Against.StringTooLong(name, Constants.DefaultNameMaxLength);
        Strength = Guard.Against.OutOfRange(strength, "Strength", 1, 10);
    }
}