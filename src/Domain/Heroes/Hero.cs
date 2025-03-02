using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Domain.Heroes;

// For strongly typed IDs, check out the rule: https://www.ssw.com.au/rules/do-you-use-strongly-typed-ids/
[ValueObject<Guid>]
public readonly partial struct HeroId;

public class Hero : AggregateRoot<HeroId>
{
    private readonly List<Power> _powers = [];

    private string _name = null!;
    private string _alias = null!;

    public string Name
    {
        get => _name;
        set
        {
            ThrowIfNullOrWhiteSpace(value, nameof(Name));
            _name = value;
        }
    }

    public string Alias
    {
        get => _alias;
        set
        {
            ThrowIfNullOrWhiteSpace(value, nameof(Alias));
            _alias = value;
        }
    }

    public int PowerLevel { get; private set; }
    public TeamId? TeamId { get; private set; }

    public IReadOnlyList<Power> Powers => _powers.AsReadOnly();

    private Hero() { }

    public static Hero Create(string name, string alias)
    {
        Guid.CreateVersion7();
        var hero = new Hero { Id = HeroId.From(Guid.CreateVersion7()), Name = name, Alias = alias };

        return hero;
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