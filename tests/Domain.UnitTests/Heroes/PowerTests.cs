using SSW.CleanArchitecture.Domain.Heroes;

namespace SSW.CleanArchitecture.Domain.UnitTests.Heroes;

public class PowerTests
{
    [Test]
    public void Power_ShouldBeCreatable()
    {
        // Arrange
        var name = "PowerName";
        var powerLevel = 5;

        // Act
        var power = new Power(name, powerLevel);

        // Assert
        power.Should().NotBeNull();
        power.Name.Should().Be(name);
        power.PowerLevel.Should().Be(powerLevel);
    }

    [Test]
    public void Power_ShouldBeComparable()
    {
        // Arrange
        var name = "PowerName";
        var powerLevel = 5;
        var power1 = new Power(name, powerLevel);
        var power2 = new Power(name, powerLevel);

        // Act
        var areEqual = power1 == power2;

        // Assert
        areEqual.Should().BeTrue();
    }

    [Test]
    [Arguments(-1, true)]
    [Arguments(0, true)]
    [Arguments(1, false)]
    [Arguments(9, false)]
    [Arguments(10, false)]
    [Arguments(11, true)]
    public void Power_WithInvalidPowerLevel_ShouldThrow(int powerLevel, bool shouldThrow)
    {
        // Arrange
        var name = "PowerName";

        // Act
        var act = () => new Power(name, powerLevel);

        // Assert
        if (shouldThrow)
        {
            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
        else
        {
            act.Should().NotThrow();
        }
    }
}