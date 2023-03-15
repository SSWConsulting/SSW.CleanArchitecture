using Application.Features.TodoItems.Specifications;
using Bogus;
using Domain.Entities;

namespace Application.UnitTests.Features.TodoItems.Specifications;

public class AllTodoItemSpecTests
{
    [Fact]
    public void Should_Return_AllItems()
    {
        const int dataCount = 10;
        var entities = new Faker<TodoItem>()
            .RuleFor(t => t.Id, _ => new TodoItemId(Guid.NewGuid()))
            .RuleFor(t => t.Title, f => f.Commerce.ProductName())
            .Generate(dataCount);

        var query = new AllTodoItemSpec();

        var result = query.Evaluate(entities).ToList();

        result.Count.Should().Be(dataCount);
        result.Should().Contain(entities);
    }
}