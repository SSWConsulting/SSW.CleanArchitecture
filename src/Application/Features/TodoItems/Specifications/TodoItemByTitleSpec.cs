using Ardalis.Specification;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Features.TodoItems.Specifications;

public sealed class TodoItemByTitleSpec : Specification<TodoItem>
{
    public TodoItemByTitleSpec(string title)
    {
        Query.Where(i => i.Title == title);
    }
}