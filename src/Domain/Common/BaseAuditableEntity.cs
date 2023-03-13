namespace Domain.Common;

public class BaseAuditableEntity : BaseEntity
{
    public DateTime CreatedAt { get; set; }
    // TODO: string for user?
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    // TODO: string for user?
    public string? LastModifiedBy { get; set; }
}