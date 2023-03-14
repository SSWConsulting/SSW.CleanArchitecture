using Application.Features.TodoItems.Commands.CreateTodoItem;
using Application.IntegrationTests.TestHelpers;
using CleanArchitecture.Application.Common.Exceptions;
using Domain.Entities;

namespace Application.IntegrationTests.Features.TodoItems.Commands;

[Collection(TestingDatabaseFixture.DatabaseCollectionDefinition)]
public class CreateTodoItemTests : IClassFixture<TestingSessionFixture>
{
    private readonly TestingDatabaseFixture _fixture;

    public CreateTodoItemTests(TestingDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ShouldRequireUniqueTitle()
    {
        await _fixture.SendAsync(new CreateTodoItemCommand("Shopping"));

        var command = new CreateTodoItemCommand("Shopping");
    
        await FluentActions.Invoking(() =>
            _fixture.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ShouldCreateTodoItem()
    {
        var command = new CreateTodoItemCommand("Tasks");

        var id = await _fixture.SendAsync(command);

        TodoItem item = (await _fixture.FindAsync<TodoItem>(new TodoItemId(id)))!;

        item.Should().NotBeNull();
        item.Title.Should().Be(command.Title);
        item.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}