using Domain.Entities;

namespace Domain.UnitTests.Entities;

public class TodoItemTests
{
    [Fact]
    public void Should_Be_Creating_Parameterless()
    {
        var todoItem = new TodoItem();

        todoItem.Should().NotBeNull();
    }
}