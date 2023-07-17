using SSW.CleanArchitecture.Domain.Common.Base;

namespace SSW.CleanArchitecture.Domain.TodoItems;

public record TodoItemCompletedEvent(TodoItem Item) : DomainEvent;
