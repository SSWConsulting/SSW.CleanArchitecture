﻿using SSW.CleanArchitecture.Domain.Heroes;

namespace SSW.CleanArchitecture.Domain.Teams;

// For strongly typed IDs, check out the rule: https://www.ssw.com.au/rules/do-you-use-strongly-typed-ids/
[ValueObject<Guid>]
public readonly partial struct TeamId;

public class Team : AggregateRoot<TeamId>
{
    public string Name
    {
        get => _name;
        private set
        {
            ThrowIfNullOrWhiteSpace(value, nameof(Name));
            _name = value;
        }
    }

    public int TotalPowerLevel { get; private set; }
    public TeamStatus Status { get; private set; }

    private readonly List<Mission> _missions = [];
    public IReadOnlyList<Mission> Missions => _missions.AsReadOnly();
    private Mission? CurrentMission => _missions.FirstOrDefault(m => m.Status == MissionStatus.InProgress);

    private readonly List<Hero> _heroes = [];
    private string _name = null!;
    public IReadOnlyList<Hero> Heroes => _heroes.AsReadOnly();

    private Team() { }

    public static Team Create(string name)
    {
        var team = new Team { Id = TeamId.From(Guid.CreateVersion7()), Name = name, Status = TeamStatus.Available };
        return team;
    }

    public void AddHero(Hero hero)
    {
        ThrowIfNull(hero);
        _heroes.Add(hero);
        TotalPowerLevel += hero.PowerLevel;
    }

    public void RemoveHero(Hero hero)
    {
        ThrowIfNull(hero);
        if (_heroes.Contains(hero))
        {
            TotalPowerLevel -= hero.PowerLevel;
            _heroes.Remove(hero);
        }
    }

    public ErrorOr<Success> ExecuteMission(string description)
    {
        ThrowIfNullOrWhiteSpace(description);

        if (Status != TeamStatus.Available)
        {
            return TeamErrors.NotAvailable;
        }

        var mission = Mission.Create(description);
        _missions.Add(mission);
        Status = TeamStatus.OnMission;

        return new Success();
    }

    public ErrorOr<Success> CompleteCurrentMission()
    {
        if (Status != TeamStatus.OnMission)
        {
            return TeamErrors.NotOnMission;
        }

        if (CurrentMission is null)
        {
            return TeamErrors.NotOnMission;
        }

        var result = CurrentMission.Complete();
        if (result.IsError)
            return result;

        Status = TeamStatus.Available;

        return new Success();
    }

    public void ReCalculatePowerLevel()
    {
        TotalPowerLevel = _heroes.Sum(h => h.PowerLevel);
    }
}