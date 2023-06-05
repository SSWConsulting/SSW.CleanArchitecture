using SSW.CleanArchitecture.Domain.Entities;

namespace SSW.CleanArchitecture.Domain.Events;

public record TodoItemCreatedEvent(TodoItem Item) : BaseEvent;
