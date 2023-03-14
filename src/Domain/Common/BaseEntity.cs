namespace Domain.Common;

public class BaseEntity<TId>
{
    public required TId Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? CreatedBy { get; set; } // TODO: String as userId?
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; } // TODO: String as userId?
}