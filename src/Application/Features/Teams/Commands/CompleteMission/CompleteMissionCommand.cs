using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.Features.Teams.Commands.CompleteMission;

public sealed record CompleteMissionCommand(Guid TeamId) : IRequest<ErrorOr<Success>>;

// ReSharper disable once UnusedType.Global

public sealed class CompleteMissionCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<CompleteMissionCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(CompleteMissionCommand request, CancellationToken cancellationToken)
    {
        var teamId = new TeamId(request.TeamId);
        var team = dbContext.Teams
            .WithSpecification(new TeamByIdSpec(teamId))
            .FirstOrDefault();

        if (team is null)
            return Error.NotFound($"Team {teamId.Value}");

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