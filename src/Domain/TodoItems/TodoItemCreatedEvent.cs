using SSW.CleanArchitecture.Domain.Common.Base;

namespace SSW.CleanArchitecture.Domain.TodoItems;

public readonly record struct TodoItemCreatedEvent(TodoItem Item) : IDomainEvent;
