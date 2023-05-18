namespace SSW.CleanArchitecture.Application.Features.TodoItems.Queries.GetAllTodoItems;

public class TodoItemDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public bool Done { get; set; }
}