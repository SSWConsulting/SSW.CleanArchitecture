using Ardalis.Specification;
using Domain.Entities;

namespace Application.Features.TodoItems.Specifications;

public sealed class TodoItemByTitleSpec : Specification<TodoItem>
{
    public TodoItemByTitleSpec(string title)
    {
        Query.Where(i => i.Title == title);
    }
}