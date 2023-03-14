namespace Domain.Common;

public abstract class AuditableEntity
{
    public DateTimeOffset CreatedAt { get; set; }
    public string? CreatedBy { get; set; } // TODO: String as userId?
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; } // TODO: String as userId?
}