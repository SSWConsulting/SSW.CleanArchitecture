using Domain.Entities;

namespace Application.Features.TodoItems.Commands.CreateTodoItem;

public record TodoItemCreatedEvent(TodoItem Item) : INotification;