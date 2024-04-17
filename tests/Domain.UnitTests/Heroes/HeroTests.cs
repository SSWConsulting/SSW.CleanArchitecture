using SSW.CleanArchitecture.Domain.Heroes;

namespace SSW.CleanArchitecture.Domain.UnitTests.Heroes;

public class HeroTests
{
    [Theory]
    [InlineData("c8ad9974-ca93-44a5-9215-2f4d9e866c7a", "cc3431a8-4a31-4f76-af64-e8198279d7a4", false)]
    [InlineData("c8ad9974-ca93-44a5-9215-2f4d9e866c7a", "c8ad9974-ca93-44a5-9215-2f4d9e866c7a", true)]
    public void HeroId_ShouldBeComparable(string stringGuid1, string stringGuid2, bool isEqual)
    {
        // Arrange
        Guid guid1 = Guid.Parse(stringGuid1);
        Guid guid2 = Guid.Parse(stringGuid2);
        HeroId id1 = new(guid1);
        HeroId id2 = new(guid2);

        // Act
        var areEqual = id1 == id2;

        // Assert
        areEqual.Should().Be(isEqual);
        id1.Value.Should().Be(guid1);
        id2.Value.Should().Be(guid2);
    }

    [Fact]
    public void Create_WithValidNameAndAlias_ShouldSucceed()
    {
        // Arrange
        var name = "name";
        var alias = "alias";

        // Act
        var hero = Hero.Create(name, alias);

        // Assert
        hero.Should().NotBeNull();
        hero.Name.Should().Be(name);
        hero.Alias.Should().Be(alias);
    }

    [Fact]
    public void Create_WithSameNameAndAlias_ShouldThrow()
    {
        // Arrange
        var name = "name";
        var alias = "name";
        var parameterName = "alias";

        // Act
        Action act = () => Hero.Create(name, alias);

        // Assert
        act.Should().Throw<ArgumentException>("Alias cannot be the same as the name")
            .WithMessage($"Input {parameterName} did not satisfy the options*")
            .WithParameterName(parameterName);
    }


    [Theory]
    [InlineData(null, "alias")]
    [InlineData("name", null)]
    [InlineData(null, null)]
    public void Create_WithNullTitleOrAlias_ShouldThrow(string? name, string? alias)
    {
        // Arrange

        // Act
        Action act = () => Hero.Create(name!, alias!);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Value cannot be null*");
    }

    [Fact]
    public void AddPower_ShouldUpdateHeroPowerLevel()
    {
        // Act
        var hero = Hero.Create("name", "alias");
        var powers = new List<Power> { new("Super-strength", 10), new("Super-speed", 5) };
        hero.UpdatePowers(powers);

        // Assert
        hero.PowerLevel.Should().Be(15);
        hero.Powers.Should().HaveCount(2);
    }

    [Fact]
    public void RemovePower_ShouldUpdateHeroPowerLevel()
    {
        // Act
        var hero = Hero.Create("name", "alias");
        var powers = new List<Power> { new("Super-strength", 10), new("Super-speed", 5) };
        hero.UpdatePowers(powers);

        // Act
        hero.UpdatePowers([new("Super-strength", 5)]);

        // Assert
        hero.PowerLevel.Should().Be(5);
        hero.Powers.Should().HaveCount(1);
    }

    [Fact]
    public void AddPower_ShouldRaisePowerLevelUpdatedEvent()
    {
        // Act
        var hero = Hero.Create("name", "alias");
        hero.Id = new HeroId(Guid.NewGuid());
        hero.UpdatePowers([new Power("Super-strength", 10)]);

        // Assert
        hero.DomainEvents.Should().NotBeNull();
        hero.DomainEvents.Should().HaveCount(1);
        hero.DomainEvents.First().Should().BeOfType<PowerLevelUpdatedEvent>()
            .Which.Invoking(e =>
            {
                e.PowerLevel.Should().Be(10);
                e.Id.Should().Be(hero.Id);
                e.Name.Should().Be(hero.Name);
            }).Invoke();
        hero.Powers.Should().ContainSingle("Super-strength");
    }

    [Fact]
    public void RemovePower_ShouldRaisePowerLevelUpdatedEvent()
    {
        // Act
        var hero = Hero.Create("name", "alias");
        var power = new Power("Super-strength", 10);
        hero.UpdatePowers([power]);

        // Assert
        hero.DomainEvents.Should().NotBeNull();
        hero.DomainEvents.Should().HaveCount(1);
        hero.DomainEvents.Should().AllSatisfy(e => e.Should().BeOfType<PowerLevelUpdatedEvent>());
        hero.DomainEvents.Last()
            .As<PowerLevelUpdatedEvent>()
            .PowerLevel.Should().Be(10);
        hero.Powers.Should().HaveCount(1);
    }
}