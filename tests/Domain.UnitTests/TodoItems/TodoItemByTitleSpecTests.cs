using SSW.CleanArchitecture.Domain.TodoItems;

namespace SSW.CleanArchitecture.Domain.UnitTests.TodoItems;

public class TodoItemByTitleSpecTests
{
    private readonly List<TodoItem> _entities =
    [
        TodoItem.Create("Apple"),
        TodoItem.Create("Banana"),
        TodoItem.Create("Apple 2"),
        TodoItem.Create("Banana 2"),
        TodoItem.Create("Hello world 2")
    ];

    [Theory]
    [InlineData("Apple")]
    [InlineData("Banana")]
    [InlineData("Apple 2")]
    [InlineData("Banana 2")]
    public void Query_ShouldReturnByTitle(string textToSearch)
    {
        var query = new TodoItemByTitleSpec(textToSearch);
        var result = query.Evaluate(_entities).ToList();

        result.Count.Should().Be(1);
        result.First().Title.Should().Be(textToSearch);
    }
}