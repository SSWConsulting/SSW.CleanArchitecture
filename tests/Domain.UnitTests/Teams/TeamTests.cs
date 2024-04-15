using SSW.CleanArchitecture.Domain.Heroes;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Domain.UnitTests.Teams;

public class TeamTests
{
    [Fact]
    public void Create_WithValidNameAndAlias_ShouldSucceed()
    {
        // Arrange
        var name = "name";

        // Act
        var team = Team.Create(name);

        // Assert
        team.Should().NotBeNull();
        team.Name.Should().Be(name);
    }
    
    [Fact]
    public void Create_WithNullNameAndAlias_ShouldThrow()
    {
        // Arrange
        string? name = null;

        // Act
        Action act = () => Team.Create(name!);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Value cannot be null. (Parameter 'name')");
    }
    
    [Fact]
    public void AddHero_ShouldUpdateTeamPowerLevel()
    {
        // Arrange
        Hero hero1 = Hero.Create("hero1", "alias1");
        Hero hero2 = Hero.Create("hero2", "alias2");
        hero1.AddPower(new Power("Foo", 10));
        hero2.AddPower(new Power("Bar", 4));
        var team = Team.Create("name");
        
        // Act
        team.AddHero(hero1);
        team.AddHero(hero2);

        // Assert
        team.TotalPowerLevel.Should().Be(14);
    }
    
    [Fact]
    public void RemoveHero_ShouldUpdateTeamPowerLevel()
    {
        // Arrange
        Hero hero1 = Hero.Create("hero1", "alias1");
        Hero hero2 = Hero.Create("hero2", "alias2");
        hero1.AddPower(new Power("Foo", 10));
        hero2.AddPower(new Power("Bar", 4));
        var team = Team.Create("name");
        team.AddHero(hero1);
        team.AddHero(hero2);
        
        // Act
        team.RemoveHero(hero1);

        // Assert
        team.TotalPowerLevel.Should().Be(4);
    }

    [Fact]
    public void ExecuteMission_ShouldUpdateTeamStatus()
    {
        // Arrange
        var team = Team.Create("name");
        
        // Act
        team.ExecuteMission("Mission");

        // Assert
        team.Status.Should().Be(TeamStatus.OnMission);
    }
    
    [Fact]
    public void CompleteCurrentMission_ShouldUpdateTeamStatus()
    {
        // Arrange
        var team = Team.Create("name");
        team.ExecuteMission("Mission");
        
        // Act
        team.CompleteCurrentMission();

        // Assert
        team.Status.Should().Be(TeamStatus.Available);
    }
    
    [Fact]
    public void CompleteCurrentMission_WhenNoMissionHasBeenExecuted_ShouldThrow()
    {
        // Arrange
        var team = Team.Create("name");
        
        // Act
        var act = () => team.CompleteCurrentMission();

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("The team is currently not on a mission.");
    }
}