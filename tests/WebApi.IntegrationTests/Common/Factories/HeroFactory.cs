using Bogus;
using SSW.CleanArchitecture.Domain.Heroes;

namespace WebApi.IntegrationTests.Common.Factories;

public static class HeroFactory
{
    private static readonly Faker<Power> PowerFaker =
        new Faker<Power>().CustomInstantiator(f => new Power(f.Commerce.Product(), f.Random.Number(1, 10)));
    
    private static readonly Faker<Hero> HeroFaker = new Faker<Hero>().CustomInstantiator(f =>
    {
        var hero = Hero.Create(
            f.Company.CompanyName(),
            f.Company.Bs()
        );

        PowerFaker
            .Generate(f.Random.Number(1, 5))
            .ForEach(power => hero.AddPower(power));
        return hero;
    });

    public static Hero Generate() => HeroFaker.Generate();

    public static IEnumerable<Hero> Generate(int count) => HeroFaker.Generate(count);
}