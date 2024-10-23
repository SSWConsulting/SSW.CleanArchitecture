﻿using SSW.CleanArchitecture.Domain.Common.Base;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Domain.Heroes;

// For strongly typed IDs, check out the rule: https://www.ssw.com.au/rules/do-you-use-strongly-typed-ids/
public readonly record struct HeroId(Guid Value);

public class Hero : AggregateRoot<HeroId>
{
    private readonly List<Power> _powers = [];
    public string Name { get; private set; } = null!;
    public string Alias { get; private set; } = null!;
    public int PowerLevel { get; private set; }
    public TeamId? TeamId { get; private set; }

    public IReadOnlyList<Power> Powers => _powers.AsReadOnly();

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
        ThrowIfNullOrWhiteSpace(name);
        Name = name;
    }

    public void UpdateAlias(string alias)
    {
        ThrowIfNullOrWhiteSpace(alias);
        Alias = alias;
    }

    public void UpdatePowers(IEnumerable<Power> updatedPowers)
    {
        _powers.Clear();
        PowerLevel = 0;

        foreach (var heroPowerModel in updatedPowers)
            AddPower(new Power(heroPowerModel.Name, heroPowerModel.PowerLevel));

        AddDomainEvent(new PowerLevelUpdatedEvent(this));
    }

    private void AddPower(Power power)
    {
        ThrowIfNull(power);

        if (!_powers.Contains(power))
        {
            _powers.Add(power);
        }

        PowerLevel += power.PowerLevel;
    }
}