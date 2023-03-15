namespace Domain.Entities;


public record TodoItemId(Guid Value);

public class TodoItem : BaseEntity<TodoItemId>
{
    public string? Title { get; set; }
    public string? Note { get; set; }
    public PriorityLevel Priority { get; set; }
    public DateTime Reminder { get; set; }
    public bool Done { get; set; }
}