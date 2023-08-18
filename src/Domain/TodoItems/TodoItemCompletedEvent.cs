using SSW.CleanArchitecture.Domain.Common.Base;

namespace SSW.CleanArchitecture.Domain.TodoItems;

public readonly record struct TodoItemCompletedEvent(TodoItem Item) : IDomainEvent;
