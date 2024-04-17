using Ardalis.GuardClauses;
using SSW.CleanArchitecture.Domain.Common.Base;

namespace SSW.CleanArchitecture.Domain.Heroes;

// For strongly typed IDs, check out the rule: https://www.ssw.com.au/rules/do-you-use-strongly-typed-ids/
public readonly record struct HeroId(Guid Value);

public class Hero : AggregateRoot<HeroId>
{
    private readonly List<Power> _powers = [];
    public string Name { get; private set; } = null!;
    public string Alias { get; private set; } = null!;
    public int PowerLevel { get; private set; }
    public IEnumerable<Power> Powers => _powers.AsReadOnly();
    
    private Hero() { }

    public static Hero Create(string name, string alias)
    {
        var hero = new Hero { Id = new HeroId(Guid.NewGuid()) };
        hero.UpdateName(name);
        hero.UpdateAlias(alias);

        return hero;
    }
    
    public void UpdateName(string name)
    {
        Guard.Against.NullOrWhiteSpace(name);
        Name = name;
    }
    
    public void UpdateAlias(string alias)
    {
        Guard.Against.NullOrWhiteSpace(alias);
        Guard.Against.InvalidInput(alias, nameof(alias), input => !input.Equals(Name, StringComparison.OrdinalIgnoreCase));
        Alias = alias;
    }

    public void UpdatePowers(IEnumerable<Power> updatedPowers)
    {
        _powers.Clear();
        PowerLevel = 0;

        foreach (var heroPowerModel in updatedPowers)
        {
            AddPower(new Power(heroPowerModel.Name, heroPowerModel.PowerLevel));
        }

        AddDomainEvent(new PowerLevelUpdatedEvent(this));
    }

    private void AddPower(Power power)
    {
        Guard.Against.Null(power);

        if (!_powers.Contains(power))
        {
            _powers.Add(power);
        }

        PowerLevel += power.PowerLevel;
    }
}