using Bogus;
using SSW.CleanArchitecture.Domain.Teams;

namespace WebApi.IntegrationTests.Common.Factories;

public static class TeamFactory
{
    private static readonly Faker<Team> _teamFaker = new Faker<Team>().CustomInstantiator(f =>
    {
        var team = Team.Create(
            f.Company.CompanyName()
        );

        return team;
    });

    public static Team Generate() => _teamFaker.Generate();

    public static IReadOnlyList<Team> Generate(int count) => _teamFaker.Generate(count);
}