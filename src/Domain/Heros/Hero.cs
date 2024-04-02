using Ardalis.GuardClauses;
using SSW.CleanArchitecture.Domain.Common.Base;
using SSW.CleanArchitecture.Domain.Common.Interfaces;

namespace SSW.CleanArchitecture.Domain.Heros;

// For strongly typed IDs, check out the rule: https://www.ssw.com.au/rules/do-you-use-strongly-typed-ids/
public readonly record struct HeroId(Guid Value);

public class Hero : AggregateRoot<HeroId>
{
    private readonly List<Power> _powers = [];
    public string Name { get; private set; } = null!;
    public string Alias { get; private set; } = null!;
    public int Strength { get; private set; }

    public IEnumerable<Power> Powers => _powers.AsReadOnly();

    public static Hero Create(string name, string alias, int strength)
    {
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.NullOrWhiteSpace(alias);

        var hero = new Hero { Id = new HeroId(Guid.NewGuid()), Name = name, Alias = alias, };
        hero.UpdateStrength(strength);

        return hero;
    }

    public void UpdateStrength(int strength)
    {
        Guard.Against.OutOfRange(strength, nameof(strength), 1, 10);
        Strength = strength;

        AddDomainEvent(new StrengthUpdatedEvent(this));
    }

    public void AddPower(string powerName)
    {
        Guard.Against.NullOrWhiteSpace(powerName, nameof(powerName));

        var power = Power.AllPowers.FirstOrDefault(p => p.Name == powerName);
        if (power is null)
            throw new ArgumentException("Invalid power name");

        if (!_powers.Contains(power))
        {
            _powers.Add(power);
        }
    }

    public void RemovePower(string powerName)
    {
        Guard.Against.NullOrWhiteSpace(powerName, nameof(powerName));

        var power = Power.AllPowers.FirstOrDefault(p => p.Name == powerName);
        if (power is null)
            throw new ArgumentException("Invalid power name");

        if (_powers.Contains(power))
        {
            _powers.Remove(power);
        }
    }
}