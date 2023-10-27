using SSW.CleanArchitecture.Domain.Common.Base;

namespace SSW.CleanArchitecture.Domain.TodoItems;

// For strongly typed IDs, check out the rule: https://www.ssw.com.au/rules/do-you-use-strongly-typed-ids/
public readonly record struct TodoItemId(Guid Value);

public class TodoItem : BaseEntity<TodoItemId>
{
    // NOTE: private setters for behavior we want to encapsulate, and public setters for properties that don't have behavior

    public string? Title { get; private set; }
    public string? Note { get; set; }
    public PriorityLevel Priority { get; set; }
    public DateTime Reminder { get; set; }
    public bool Done { get; private set; }

    // Needed for EF
    private TodoItem() { }

    public static TodoItem Create(string title)
    {
        ArgumentException.ThrowIfNullOrEmpty(title, nameof(title));

        var todoItem = new TodoItem
        {
            Title = title,
            Priority = PriorityLevel.None,
            Done = false
        };

        todoItem.AddDomainEvent(new TodoItemCreatedEvent(todoItem));

        return todoItem;
    }

    public static TodoItem Create(string title, string note, PriorityLevel priority, DateTime reminder)
    {
        var todoItem = Create(title);
        todoItem.Note = note;
        todoItem.Priority = priority;
        todoItem.Reminder = reminder;

        return todoItem;
    }

    public void Complete()
    {
        Done = true;

        AddDomainEvent(new TodoItemCompletedEvent(this));
    }
}