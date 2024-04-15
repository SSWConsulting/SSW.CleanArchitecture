using SSW.CleanArchitecture.Domain.Heroes;

namespace SSW.CleanArchitecture.Domain.UnitTests.Heroes;

public class HeroesTests
{
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
        hero.AddPower(new Power("Super-strength", 10));
        hero.AddPower(new Power("Super-speed", 5));

        // Assert
        hero.PowerLevel.Should().Be(15);
    }

    [Fact]
    public void RemovePower_ShouldUpdateHeroPowerLevel()
    {
        // Act
        var hero = Hero.Create("name", "alias");
        hero.AddPower(new Power("Super-strength", 10));
        hero.AddPower(new Power("Super-speed", 5));
        hero.RemovePower("Super-strength");

        // Assert
        hero.PowerLevel.Should().Be(5);
    }

    [Fact]
    public void AddPower_ShouldRaisePowerLevelUpdatedEvent()
    {
        // Act
        var hero = Hero.Create("name", "alias");
        hero.AddPower(new Power("Super-strength", 10));

        // Assert
        hero.DomainEvents.Should().NotBeNull();
        hero.DomainEvents.Should().HaveCount(1);
        hero.DomainEvents.First().Should().BeOfType<PowerLevelUpdatedEvent>();
        hero.DomainEvents.First()
            .As<PowerLevelUpdatedEvent>()
            .PowerLevel.Should().Be(10);
    }

    [Fact]
    public void RemovePower_ShouldRaisePowerLevelUpdatedEvent()
    {
        // Act
        var hero = Hero.Create("name", "alias");
        var powerName = "Super-strength";
        hero.AddPower(new Power(powerName, 10));
        hero.RemovePower(powerName);

        // Assert
        hero.DomainEvents.Should().NotBeNull();
        hero.DomainEvents.Should().HaveCount(2);
        hero.DomainEvents.Should().AllSatisfy(e => e.Should().BeOfType<PowerLevelUpdatedEvent>());
        hero.DomainEvents.Last()
            .As<PowerLevelUpdatedEvent>()
            .PowerLevel.Should().Be(0);
    }
}