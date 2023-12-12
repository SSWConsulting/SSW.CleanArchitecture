using NSubstitute.Core;
using SSW.CleanArchitecture.Application.Features.TodoItems.Queries.GetAllTodoItems;
using System.Net.Http.Json;
using WebApi.IntegrationTests.Common.Factories;
using WebApi.IntegrationTests.Common.Fixtures;

namespace WebApi.IntegrationTests.Endpoints.TodoItems.Queries.GetAllTodoItems;

public class GetAllTodoItemsQueryTests(TestingDatabaseFixture fixture, ITestOutputHelper output)
    : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task Query_ShouldReturnAllTodoItems()
    {
        // Arrange
        const int entityCount = 10;
        var entities = TodoItemFactory.Generate(entityCount);
        await AddEntitiesAsync(entities);
        var client = GetAnonymousClient();

        // Act
        var result = await client.GetFromJsonAsync<TodoItemDto[]>("/todoitems");

        // Assert
        result.Should().NotBeNull();
        result!.Length.Should().Be(entityCount);
    }
}