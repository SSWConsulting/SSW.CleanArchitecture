using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.Features.Teams.Commands.ExecuteMission;

public sealed record ExecuteMissionCommand(Guid TeamId, string Description) : IRequest<Result>;

// ReSharper disable once UnusedType.Global
public sealed class ExecuteMissionCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<ExecuteMissionCommand, Result>
{
    public async Task<Result> Handle(ExecuteMissionCommand request, CancellationToken cancellationToken)
    {
        var teamId = new TeamId(request.TeamId);
        var team = dbContext.Teams
            .WithSpecification(new TeamByIdSpec(teamId))
            .FirstOrDefault();

        if (team is null)
            return Result.NotFound($"Team {teamId.Value}");

        team.ExecuteMission(request.Description);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

public class ExecuteMissionCommandValidator : AbstractValidator<ExecuteMissionCommand>
{
    public ExecuteMissionCommandValidator()
    {
        RuleFor(v => v.TeamId)
            .NotEmpty();

        RuleFor(v => v.Description)
            .NotEmpty();
    }
}