using Ardalis.Specification;
using SSW.CleanArchitecture.Domain.TodoItems;

namespace SSW.CleanArchitecture.Application.Features.TodoItems.Specifications;

public sealed class TodoItemByIdSpec : SingleResultSpecification<TodoItem>
{
    public TodoItemByIdSpec(TodoItemId todoItemId)
    {
        Query.Where(t => t.Id == todoItemId);
    }
}