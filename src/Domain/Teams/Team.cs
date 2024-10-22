﻿using Ardalis.GuardClauses;
using ErrorOr;
using SSW.CleanArchitecture.Domain.Common;
using SSW.CleanArchitecture.Domain.Common.Base;
using SSW.CleanArchitecture.Domain.Heroes;

namespace SSW.CleanArchitecture.Domain.Teams;

// For strongly typed IDs, check out the rule: https://www.ssw.com.au/rules/do-you-use-strongly-typed-ids/
public sealed record TeamId(Guid Value);

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

    public ErrorOr<Success> ExecuteMission(string description)
    {
        Guard.Against.NullOrWhiteSpace(description, nameof(description));

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

        CurrentMission.Complete();
        Status = TeamStatus.Available;

        return new Success();
    }

    public void ReCalculatePowerLevel()
    {
        TotalPowerLevel = _heroes.Sum(h => h.PowerLevel);
    }
}