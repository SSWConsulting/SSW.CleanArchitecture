using Bogus;
using SSW.CleanArchitecture.Domain.Heroes;

namespace WebApi.IntegrationTests.Common.Factories;

public static class HeroFactory
{
    private static readonly Faker<Power> _powerFaker =
        new Faker<Power>().CustomInstantiator(f => new Power(f.Commerce.Product(), f.Random.Number(1, 10)));

    private static readonly Faker<Hero> _heroFaker = new Faker<Hero>().CustomInstantiator(f =>
    {
        var hero = Hero.Create(
            f.Person.FullName,
            f.Person.FirstName
        );

        var powers = _powerFaker
            .Generate(f.Random.Number(1, 5));

        hero.UpdatePowers(powers);

        return hero;
    });

    public static Hero Generate() => _heroFaker.Generate();

    public static IEnumerable<Hero> Generate(int count) => _heroFaker.Generate(count);
}