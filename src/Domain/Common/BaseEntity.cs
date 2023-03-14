namespace Domain.Common;

public abstract class BaseEntity<TId> : AuditableEntity
{
    public TId Id { get; set; }
}