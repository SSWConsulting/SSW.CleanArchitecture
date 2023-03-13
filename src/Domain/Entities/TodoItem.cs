namespace Domain.Entities;

public class TodoItem : BaseEntity
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Note { get; set; }
    public PriorityLevel Priority { get; set; }
    public DateTime? Reminder { get; set; }
    public bool Done { get; set; }
}