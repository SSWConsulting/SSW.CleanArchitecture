using SSW.CleanArchitecture.Domain.Common.Base;
using SSW.CleanArchitecture.Domain.Entities;

namespace SSW.CleanArchitecture.Domain.Events;

public record TodoItemCompletedEvent(TodoItem Item) : DomainEvent;
