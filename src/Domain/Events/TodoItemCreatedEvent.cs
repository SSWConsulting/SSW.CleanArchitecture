using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Events;

public record TodoItemCreatedEvent(TodoItem Item) : BaseEvent;
