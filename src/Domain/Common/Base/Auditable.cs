namespace SSW.CleanArchitecture.Domain.Common.Base;

/// <summary>
/// Tracks creation and modification of objects.
/// </summary>
public abstract class Auditable : IAuditable
{
    public const int CreatedByMaxLength = 128;
    public const int UpdatedByMaxLength = 128;

    private const string SystemUser = "System";
    private string _createdBy = null!;
    private string? _updatedBy;

    public DateTimeOffset CreatedAt { get; private set; }

    public string CreatedBy
    {
        get => _createdBy;
        private set
        {
            ThrowIfNullOrWhiteSpace(value, nameof(CreatedBy));
            ThrowIfGreaterThan(value.Length, CreatedByMaxLength, nameof(CreatedBy));
            _createdBy = value;
        }
    }

    public DateTimeOffset? UpdatedAt { get; private set; }

    public string? UpdatedBy
    {
        get => _updatedBy;
        private set
        {
            ThrowIfNullOrWhiteSpace(value, nameof(UpdatedBy));
            ThrowIfGreaterThan(value.Length, UpdatedByMaxLength, nameof(UpdatedBy));
            _updatedBy = value;
        }
    }

    public void SetCreated(DateTimeOffset createdAt, string? createdBy)
    {
        CreatedAt = createdAt;
        CreatedBy = createdBy ?? SystemUser;
    }

    public void SetUpdated(DateTimeOffset updatedAt, string? updatedBy)
    {
        UpdatedAt = updatedAt;
        UpdatedBy = updatedBy ?? SystemUser;
    }
}