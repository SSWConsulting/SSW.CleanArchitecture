using Application.Features.TodoItems.Commands.CreateTodoItem;
using Application.IntegrationTests.TestHelpers;
using CleanArchitecture.Application.Common.Exceptions;
using Domain.Entities;

namespace Application.IntegrationTests.Features.TodoItems.Commands;

public class CreateTodoItemTests : IntegrationTestBase
{
    public CreateTodoItemTests(TestingDatabaseFixture fixture) : base(fixture) { }
    
    [Fact]
    public async Task ShouldRequireUniqueTitle()
    {
        await Fixture.SendAsync(new CreateTodoItemCommand("Shopping"));

        var command = new CreateTodoItemCommand("Shopping");
    
        await FluentActions.Invoking(() =>
            Fixture.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ShouldCreateTodoItem()
    {
        var command = new CreateTodoItemCommand("Tasks");

        var id = await Fixture.SendAsync(command);

        TodoItem item = (await Fixture.FindAsync<TodoItem>(new TodoItemId(id)))!;

        item.Should().NotBeNull();
        item.Title.Should().Be(command.Title);
        item.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}