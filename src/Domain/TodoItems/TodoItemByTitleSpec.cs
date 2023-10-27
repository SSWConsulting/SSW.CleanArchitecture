using Ardalis.Specification;

namespace SSW.CleanArchitecture.Domain.TodoItems;

public sealed class TodoItemByTitleSpec : Specification<TodoItem>
{
    public TodoItemByTitleSpec(string title)
    {
        Query.Where(i => i.Title == title);
    }
}
