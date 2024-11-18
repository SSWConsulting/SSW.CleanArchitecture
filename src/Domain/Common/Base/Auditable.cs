namespace SSW.CleanArchitecture.Domain.Common.Base;

/// <summary>
/// Tracks creation and modification of objects.
/// </summary>
public abstract class Auditable : IAuditable
{
    public DateTimeOffset CreatedAt { get; private set; }
    public string? CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public string? UpdatedBy { get; private set; }

    public void SetCreated(DateTimeOffset createdAt, string? createdBy)
    {
        CreatedAt = createdAt;
        CreatedBy = createdBy;
    }

    public void SetUpdated(DateTimeOffset updatedAt, string? updatedBy)
    {
        UpdatedAt = updatedAt;
        UpdatedBy = updatedBy;
    }
}