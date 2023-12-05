using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.Common.Exceptions;
using SSW.CleanArchitecture.Application.Features.TodoItems.Commands.CreateTodoItem;
using SSW.CleanArchitecture.Domain.TodoItems;
using System.Net;
using System.Net.Http.Json;
using WebApi.IntegrationTests.Common.Fixtures;

namespace WebApi.IntegrationTests.Endpoints.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemCommandTests(TestingDatabaseFixture fixture, ITestOutputHelper output)
    : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task Command_ShouldRequireUniqueTitle()
    {
        // Arrange
        var cmd = new CreateTodoItemCommand("Shopping");
        var client = GetAnonymousClient();
        var createTodoItem = async () => await client.PostAsJsonAsync("/todoitems", cmd);
        await createTodoItem();

        // Act
        var result = await createTodoItem();
        var validation = await result.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        validation.Should().NotBeNull();
        validation!.Errors.Should().HaveCount(1);
    }

    [Fact]
    public async Task Command_ShouldCreateTodoItem()
    {
        // Arrange
        var cmd = new CreateTodoItemCommand("Shopping");
        var client = GetAnonymousClient();

        // Act
        var result = await client.PostAsJsonAsync("/todoitems", cmd);

        // Assert
        var item = await Context.TodoItems.FirstOrDefaultAsync(t => t.Title == cmd.Title);

        item.Should().NotBeNull();
        item!.Title.Should().Be(cmd.Title);
        item.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(10));
    }
}