using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SSW.CleanArchitecture.Domain.Heroes;
using SSW.CleanArchitecture.Domain.Teams;
using SSW.CleanArchitecture.Infrastructure.Persistence;

namespace Database;

public class ApplicationDbContextInitializer(
    ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext dbContext)
{
    private readonly string[] _superHeroNames =
    [
        "Superman",
        "Batman",
        "Wonder Woman",
        "Flash", "Aquaman",
        "Cyborg",
        "Green Lantern",
        "Shazam",
        "Captain Marvel",
        "Cyclops",
        "Wolverine",
        "Storm"
    ];

    private readonly string[] _superPowers =
    [
        "Strength",
        "Flight",
        "Invulnerability",
        "Speed",
        "Heat Vision",
        "X-Ray Vision",
        "Hearing",
        "Healing Factor",
        "Agility",
        "Stamina",
        "Breath",
        "Weapons",
        "Intelligence"
    ];

    private readonly string[] _missionNames =
    [
        "Save the world",
        "Rescue the hostages",
        "Defeat the villain",
        "Stop the bomb",
        "Protect the city"
    ];

    private readonly string[] _teamNames =
    [
        "Marvel",
        "Avengers",
        "DC",
        "Justice League",
        "X-Men"
    ];

    private const int NumHeroes = 1000;

    private const int NumTeams = 5;

    public async Task InitializeAsync()
    {
        try
        {
            if (dbContext.Database.IsSqlServer())
            {
                await dbContext.Database.MigrateAsync();
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while migrating or initializing the database");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            var heroes = await SeedHeroes();
            await SeedTeams(heroes);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while seeding the database");
            throw;
        }
    }

    private async Task<List<Hero>> SeedHeroes()
    {
        if (dbContext.Heroes.Any())
            return [];

        var faker = new Faker<Hero>()
            .CustomInstantiator(f =>
            {
                var name = f.PickRandom(_superHeroNames);
                var hero = Hero.Create(name, name.Substring(0, 2));
                var powers = f.PickRandom(_superPowers, f.Random.Number(1, 3))
                    .Select(p => new Power(p, f.Random.Number(1, 10)));
                hero.UpdatePowers(powers);
                return hero;
            });

        var heroes = faker.Generate(NumHeroes);
        await dbContext.Heroes.AddRangeAsync(heroes);
        await dbContext.SaveChangesAsync();

        return heroes;
    }

    private async Task SeedTeams(List<Hero> heroes)
    {
        if (dbContext.Teams.Any())
            return;

        var faker = new Faker<Team>()
            .CustomInstantiator(f =>
            {
                var name = f.PickRandom(_teamNames);
                var team = Team.Create(name);
                var heroesToAdd = f.PickRandom(heroes, f.Random.Number(2, 5));

                foreach (var hero in heroesToAdd)
                    team.AddHero(hero);

                var sendOnMission = f.Lorem.Random.Bool();

                if (sendOnMission)
                {
                    var missionName = f.PickRandom(_missionNames);
                    team.ExecuteMission(missionName);
                }

                return team;
            });

        var teams = faker.Generate(NumTeams);
        await dbContext.Teams.AddRangeAsync(teams);
        await dbContext.SaveChangesAsync();
    }
}