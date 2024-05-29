using Ardalis.GuardClauses;
using SSW.CleanArchitecture.Domain.Common.Base;
using SSW.CleanArchitecture.Domain.Heroes;

namespace SSW.CleanArchitecture.Domain.Teams;

// For strongly typed IDs, check out the rule: https://www.ssw.com.au/rules/do-you-use-strongly-typed-ids/
public readonly record struct TeamId(Guid Value);

public class Team : AggregateRoot<TeamId>
{
    public string Name { get; private set; } = null!;
    public int TotalPowerLevel { get; private set; }
    public TeamStatus Status { get; private set; }

    private readonly List<Mission> _missions = [];
    public IReadOnlyList<Mission> Missions => _missions.AsReadOnly();
    private Mission? CurrentMission => _missions.FirstOrDefault(m => m.Status == MissionStatus.InProgress);

    private readonly List<Hero> _heroes = [];
    public IReadOnlyList<Hero> Heroes => _heroes.AsReadOnly();

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
        TotalPowerLevel += hero.PowerLevel;
    }

    public void RemoveHero(Hero hero)
    {
        Guard.Against.Null(hero, nameof(hero));
        if (_heroes.Contains(hero))
        {
            _heroes.Remove(hero);
            TotalPowerLevel -= hero.PowerLevel;
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

    public void CompleteCurrentMission()
    {
        if (Status != TeamStatus.OnMission)
        {
            throw new InvalidOperationException("The team is currently not on a mission.");
        }

        if (CurrentMission is null)
        {
            throw new InvalidOperationException("There is no mission in progress.");
        }

        CurrentMission.Complete();
        Status = TeamStatus.Available;
    }
}