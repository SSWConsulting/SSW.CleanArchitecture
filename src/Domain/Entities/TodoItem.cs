namespace Domain.Entities;

public class TodoItem : BaseEntity
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Note { get; set; }
    public PriorityLevel Priority { get; set; }
    public DateTime? Reminder { get; set; }
    private bool _done;

    public bool Done
    {
        get => _done;
        set
        {
            if (value is true && _done is false)
            {
                AddDomainEvent(new TodoItemCompletedEvent(this));
            }

            _done = value;
        }
    }
}