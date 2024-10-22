using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.Features.Teams.Commands.CompleteMission;

public sealed record CompleteMissionCommand(Guid TeamId) : IRequest<Result>;

// ReSharper disable once UnusedType.Global

public sealed class CompleteMissionCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<CompleteMissionCommand, Result>
{
    public async Task<Result> Handle(CompleteMissionCommand request, CancellationToken cancellationToken)
    {
        var teamId = new TeamId(request.TeamId);
        var team = dbContext.Teams
            .WithSpecification(new TeamByIdSpec(teamId))
            .FirstOrDefault();

        if (team is null)
            return Result.NotFound($"Team {teamId.Value}");

        team.CompleteCurrentMission();
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

public class CompleteMissionCommandValidator : AbstractValidator<CompleteMissionCommand>
{
    public CompleteMissionCommandValidator()
    {
        RuleFor(v => v.TeamId)
            .NotEmpty();
    }
}