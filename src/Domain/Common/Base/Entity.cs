namespace SSW.CleanArchitecture.Domain.Common.Base;

/// <summary>
/// Entities have an ID and a lifecycle.  They can be created within the domain, but not externally.
/// </summary>
public abstract class Entity<TId> : AuditableEntity
{
    public TId Id { get; set; } = default!;
}