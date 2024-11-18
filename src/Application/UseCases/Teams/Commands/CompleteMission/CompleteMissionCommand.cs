using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.UseCases.Teams.Commands.CompleteMission;

public sealed record CompleteMissionCommand(Guid TeamId) : IRequest<ErrorOr<Success>>;

internal sealed class CompleteMissionCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<CompleteMissionCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(CompleteMissionCommand request, CancellationToken cancellationToken)
    {
        var teamId = TeamId.From(request.TeamId);
        var team = dbContext.Teams
            .WithSpecification(new TeamByIdSpec(teamId))
            .FirstOrDefault();

        if (team is null)
            return TeamErrors.NotFound;

        team.CompleteCurrentMission();
        await dbContext.SaveChangesAsync(cancellationToken);

        return new Success();
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