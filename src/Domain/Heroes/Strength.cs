using Ardalis.GuardClauses;
using SSW.CleanArchitecture.Domain.Common.Interfaces;

namespace SSW.CleanArchitecture.Domain.Heroes;

/// <summary>
/// Strength can only be from 1 to 10
/// </summary>
public record Strength : IValueObject
{
    public int Value { get; }

    public Strength(int value)
    {
        Guard.Against.OutOfRange(Value, "Strength", 1, 10);
        Value = value;
    }
}