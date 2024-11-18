using SSW.CleanArchitecture.Domain.Teams;
using Vogen;

namespace SSW.CleanArchitecture.Domain.Heroes;

// For strongly typed IDs, check out the rule: https://www.ssw.com.au/rules/do-you-use-strongly-typed-ids/
[ValueObject<Guid>]
public readonly partial struct HeroId;

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
        Guid.CreateVersion7();
        var hero = new Hero { Id = HeroId.From(Guid.CreateVersion7()) };
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