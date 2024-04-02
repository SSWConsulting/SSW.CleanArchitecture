using Ardalis.GuardClauses;
using SSW.CleanArchitecture.Domain.Common.Base;
using SSW.CleanArchitecture.Domain.Heros;

namespace SSW.CleanArchitecture.Domain.Teams;

// For strongly typed IDs, check out the rule: https://www.ssw.com.au/rules/do-you-use-strongly-typed-ids/
public readonly record struct TeamId(Guid Value);

public class Team : AggregateRoot<TeamId>
{
    public string Name { get; private set; } = null!;
    public int TotalStrength { get; private set; }
    public TeamStatus Status { get; private set; }

    private readonly List<Mission> _missions = [];
    public IEnumerable<Mission> Missions => _missions.AsReadOnly();

    private readonly List<Hero> _heroes = [];
    public IEnumerable<Hero> Heroes => _heroes.AsReadOnly();

    private Team() { }

    public static Team Create(string name)
    {
        Guard.Against.NullOrWhiteSpace(name);

        var team = new Team { Id = new TeamId(Guid.NewGuid()), Name = name, Status = TeamStatus.Available };

        return team;
    }

    public void AddHero(Hero hero)
    {
        Guard.Against.Null(hero, nameof(hero));
        _heroes.Add(hero);
        TotalStrength += hero.Strength;
    }

    public void RemoveHero(Hero hero)
    {
        Guard.Against.Null(hero, nameof(hero));
        if (_heroes.Contains(hero))
        {
            _heroes.Remove(hero);
            TotalStrength -= hero.Strength;
        }
    }

    public void ExecuteMission(string description)
    {
        Guard.Against.NullOrWhiteSpace(description, nameof(description));

        if (Status != TeamStatus.Available)
        {
            throw new InvalidOperationException("The team is currently not available for a new mission.");
        }

        var mission = Mission.Create(description);
        _missions.Add(mission);
        Status = TeamStatus.OnMission;
    }
}