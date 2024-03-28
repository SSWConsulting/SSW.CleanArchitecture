using Ardalis.GuardClauses;
using SSW.CleanArchitecture.Domain.Common.Base;

namespace SSW.CleanArchitecture.Domain.Teams;

// For strongly typed IDs, check out the rule: https://www.ssw.com.au/rules/do-you-use-strongly-typed-ids/
public readonly record struct MissionId(Guid Value);

public class Mission : BaseEntity<MissionId>
{
    public string Description { get; private set; } = null!;

    public MissionStatus Status { get; private set; }

    private Mission() { } // Needed for EF Core

    // NOTE: Internal so that missions can only be created by the aggregate
    internal static Mission Create(string description)
    {
        Guard.Against.NullOrWhiteSpace(description);
        return new Mission { Description = description, Status = MissionStatus.InProgress };
    }

}