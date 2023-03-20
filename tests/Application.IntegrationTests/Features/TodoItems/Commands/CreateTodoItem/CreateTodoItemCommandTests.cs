using Application.Features.TodoItems.Commands.CreateTodoItem;
using Application.IntegrationTests.TestHelpers;
using CleanArchitecture.Application.Common.Exceptions;
using Domain.Entities;

namespace Application.IntegrationTests.Features.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemCommandTests : IntegrationTestBase
{
    public CreateTodoItemCommandTests(TestingDatabaseFixture fixture) : base(fixture) { }
    
    [Fact]
    public async Task ShouldRequireUniqueTitle()
    {
        await Mediator.Send(new CreateTodoItemCommand("Shopping"));

        var command = new CreateTodoItemCommand("Shopping");
    
        await FluentActions.Invoking(() =>
            Mediator.Send(command)).Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ShouldCreateTodoItem()
    {
        var command = new CreateTodoItemCommand("Tasks");

        var id = await Mediator.Send(command);

        TodoItem item = (await Context.TodoItems.FindAsync(new TodoItemId(id)))!;

        item.Should().NotBeNull();
        item.Title.Should().Be(command.Title);
        item.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(10));
    }
}