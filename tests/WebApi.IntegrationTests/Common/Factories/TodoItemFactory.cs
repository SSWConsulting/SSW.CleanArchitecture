using Bogus;
using SSW.CleanArchitecture.Domain.TodoItems;

namespace WebApi.IntegrationTests.Common.Factories;

public static class TodoItemFactory
{
    private static readonly Faker<TodoItem> Faker = new Faker<TodoItem>().CustomInstantiator(f => TodoItem.Create(
        f.Lorem.Sentence()
    ));

    public static TodoItem Generate() => Faker.Generate();

    public static IEnumerable<TodoItem> Generate(int num) => Faker.Generate(num);
}