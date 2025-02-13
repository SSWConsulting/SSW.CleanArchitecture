namespace SSW.CleanArchitecture.Domain.Common.Interfaces;

public interface IAuditable
{
    DateTimeOffset CreatedAt { get; }
    string? CreatedBy { get; } // TODO: String as userId? (https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/76)
    DateTimeOffset? UpdatedAt { get; }
    string? UpdatedBy { get; } // TODO: String as userId? (https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/76)

    void SetCreated(DateTimeOffset createdAt, string? createdBy);

    void SetUpdated(DateTimeOffset updatedAt, string? updatedBy);
}