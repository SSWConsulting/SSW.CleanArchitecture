using Domain.Entities;

namespace Domain.Events;

public record TodoItemCreatedEvent(TodoItem Item) : BaseEvent;
