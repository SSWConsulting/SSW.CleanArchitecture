namespace SSW.CleanArchitecture.Domain.Common.Interfaces;

public interface IAuditable
{
    DateTimeOffset CreatedAt { get; }
    string CreatedBy { get; }
    DateTimeOffset? UpdatedAt { get; }
    string? UpdatedBy { get; }

    void SetCreated(DateTimeOffset createdAt, string? createdBy = null);

    void SetUpdated(DateTimeOffset updatedAt, string? updatedBy = null);
}