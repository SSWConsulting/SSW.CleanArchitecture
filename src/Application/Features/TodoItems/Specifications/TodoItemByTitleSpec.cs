using Ardalis.Specification;
using SSW.CleanArchitecture.Domain.Entities;

namespace SSW.CleanArchitecture.Application.Features.TodoItems.Specifications;

public sealed class TodoItemByTitleSpec : Specification<TodoItem>
{
    public TodoItemByTitleSpec(string title)
    {
        Query.Where(i => i.Title == title);
    }
}