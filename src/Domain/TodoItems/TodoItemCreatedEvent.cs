using SSW.CleanArchitecture.Domain.Common.Base;

namespace SSW.CleanArchitecture.Domain.TodoItems;

public record TodoItemCreatedEvent(TodoItem Item) : DomainEvent;
