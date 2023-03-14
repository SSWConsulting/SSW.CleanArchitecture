using Application.Features.TodoItems.Commands.CreateTodoItem;

namespace Application.UnitTests.Features.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemCommandTests
{
    [Theory]
    [MemberData(nameof(TitleData))]
    public void Title_Should_BeConstrained(string title, bool isValid, string reason)
    {
        var validator = new CreateTodoItemCommandValidator();

        var result = validator.Validate(new CreateTodoItemCommand(title));

        result.IsValid.Should().Be(isValid, reason);
    }

    public static IEnumerable<object[]> TitleData => new List<object[]>
    {
        new object[] { "Hello world", true, "Standard input should be valid" },
        new object[] { new string(Enumerable.Repeat('a', 200).ToArray()), true, "Must be less than or equal than 200 characters" },
        new object[] { new string(Enumerable.Repeat('a', 201).ToArray()), false, "Must be less than or equal than 200 characters" },
    };
}