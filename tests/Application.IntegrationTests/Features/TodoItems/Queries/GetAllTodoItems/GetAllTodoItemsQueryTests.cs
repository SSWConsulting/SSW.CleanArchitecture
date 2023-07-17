using Bogus;
using SSW.CleanArchitecture.Application.Features.TodoItems.Queries.GetAllTodoItems;
using SSW.CleanArchitecture.Application.IntegrationTests.TestHelpers;
using SSW.CleanArchitecture.Domain.TodoItems;

namespace SSW.CleanArchitecture.Application.IntegrationTests.Features.TodoItems.Queries.GetAllTodoItems;

public class GetAllTodoItemsQueryTests : IntegrationTestBase
{
    public GetAllTodoItemsQueryTests(TestingDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task Should_Return_All_TodoItems()
    {
        const int entityCount = 10;
        var entities = new Faker<TodoItem>()
            .CustomInstantiator(f => TodoItem.Create(f.UniqueIndex.ToString()))
            .Generate(entityCount);

        await Context.TodoItems.AddRangeAsync(entities);
        await Context.SaveChangesAsync();

        var result = await Mediator.Send(new GetAllTodoItemsQuery());

        result.Count.Should().Be(entityCount);
    }
}