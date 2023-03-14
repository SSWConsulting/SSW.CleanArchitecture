using Application.Features.TodoItems.Specifications;
using Bogus;
using Domain.Entities;

namespace Application.UnitTests.Features.TodoItems.Specifications;

public class TodoItemByTitleSpecTests
{
    private readonly List<TodoItem> _entities;
    
    public TodoItemByTitleSpecTests()
    {
        _entities = new List<TodoItem>()
        {
            new() { Id = new TodoItemId(Guid.NewGuid()), Title = "Apple", },
            new() { Id = new TodoItemId(Guid.NewGuid()), Title = "Banana", },
            new() { Id = new TodoItemId(Guid.NewGuid()), Title = "Apple 2", },
            new() { Id = new TodoItemId(Guid.NewGuid()), Title = "Banana 2", },
            new() { Id = new TodoItemId(Guid.NewGuid()), Title = "Hello world 2", },
        };
    }

    [Theory]
    [InlineData("Apple")]
    [InlineData("Banana")]
    public void Should_Return_ByTitle(string textToSearch)
    {
        var query = new TodoItemByTitleSpec(textToSearch);
        var result = query.Evaluate(_entities).ToList();

        result.Count.Should().Be(1);
        result.First().Title.Should().Be(textToSearch);
    }
}