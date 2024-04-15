using Ardalis.GuardClauses;
using SSW.CleanArchitecture.Domain.Common;
using SSW.CleanArchitecture.Domain.Common.Base;
using SSW.CleanArchitecture.Domain.Common.Interfaces;

namespace SSW.CleanArchitecture.Domain.Teams;

// For strongly typed IDs, check out the rule: https://www.ssw.com.au/rules/do-you-use-strongly-typed-ids/
public readonly record struct MissionId(Guid Value);

public class Mission : Entity<MissionId>, IAuditableEntity
{
    public string Description { get; private set; } = null!;

    public MissionStatus Status { get; private set; }

    private Mission() { } // Needed for EF Core

    // NOTE: Internal so that missions can only be created by the aggregate
    internal static Mission Create(string description)
    {
        Guard.Against.NullOrWhiteSpace(description);
        Guard.Against.StringTooLong(description, Constants.DefaultDescriptionMaxLength);
        return new Mission { Description = description, Status = MissionStatus.InProgress };
    }

    internal void Complete()
    {
        if (Status == MissionStatus.Complete)
        {
            throw new InvalidOperationException("Mission is already completed");
        }

        Status = MissionStatus.Complete;
    }
}