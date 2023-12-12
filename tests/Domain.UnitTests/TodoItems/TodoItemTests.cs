using SSW.CleanArchitecture.Domain.TodoItems;

namespace SSW.CleanArchitecture.Domain.UnitTests.TodoItems;

public class TodoItemTests
{
    [Fact]
    public void Create_WithValidTitle_ShouldSucceed()
    {
        // Arrange
        var title = "title";

        // Act
        var todoItem = TodoItem.Create(title);

        // Assert
        todoItem.Should().NotBeNull();
        todoItem.Title.Should().Be(title);
        todoItem.Priority.Should().Be(PriorityLevel.None);
    }

    [Fact]
    public void Create_WithNullTitle_ShouldThrow()
    {
        // Arrange
        string? title = null;

        // Act
        Action act = () => TodoItem.Create(title!);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Value cannot be null. (Parameter 'title')");
    }

    [Fact]
    public void Create_ShouldRaiseDomainEvent()
    {
        // Act
        var todoItem = TodoItem.Create("title");

        // Assert
        todoItem.DomainEvents.Should().NotBeNull();
        todoItem.DomainEvents.Should().HaveCount(1);
        todoItem.DomainEvents.Should().ContainSingle(x => x is TodoItemCreatedEvent);
    }

    [Fact]
    public void Complete_ShouldSetDone()
    {
        // Arrange
        var todoItem = TodoItem.Create("title");

        // Act
        todoItem.Complete();

        // Assert
        todoItem.Done.Should().BeTrue();
    }

    [Fact]
    public void Complete_ShouldRaiseDomainEvent()
    {
        // Arrange
        var todoItem = TodoItem.Create("title");

        // Act
        todoItem.Complete();

        // Assert
        todoItem.DomainEvents.Should().NotBeNull();
        todoItem.DomainEvents.Should().HaveCount(2);
        todoItem.DomainEvents.Should().ContainSingle(x => x is TodoItemCompletedEvent);
    }
}