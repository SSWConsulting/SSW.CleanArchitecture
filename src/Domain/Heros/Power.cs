using SSW.CleanArchitecture.Domain.Common.Interfaces;

namespace SSW.CleanArchitecture.Domain.Heros;

public record Power : IValueObject
{
    public string? Name { get; private set; }

    // Don't allow new powers to be created.  Only use pre-defined powers.
    private Power() { }

    public static Power SuperSpeed => new() { Name = "Super Speed" };
    public static Power SuperStrength => new() { Name = "Super Strength" };
    public static Power Flight => new() { Name = "Flight" };
    public static Power Invisibility => new() { Name = "Invisibility" };
    public static Power Telekinesis => new() { Name = "Telekinesis" };
    public static Power Telepathy => new() { Name = "Telepathy" };
    public static Power Healing => new() { Name = "Healing" };

    public static IEnumerable<Power> AllPowers => new[]
    {
        SuperSpeed, SuperStrength, Flight, Invisibility, Telekinesis, Telepathy, Healing
    };
}