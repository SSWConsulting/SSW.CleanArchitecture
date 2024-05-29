using SSW.CleanArchitecture.Application.Common.Exceptions;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.Features.Teams.Commands.CompleteMission;

public sealed record CompleteMissionCommand(Guid TeamId) : IRequest;

// ReSharper disable once UnusedType.Global
public sealed class CompleteMissionCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<CompleteMissionCommand>
{
    public async Task Handle(CompleteMissionCommand request, CancellationToken cancellationToken)
    {
        var teamId = new TeamId(request.TeamId);
        var team = dbContext.Teams
            .WithSpecification(new TeamByIdSpec(teamId))
            .FirstOrDefault();

        if (team is null)
        {
            throw new NotFoundException(nameof(Team), teamId);
        }
        
        team.CompleteCurrentMission();
        await dbContext.SaveChangesAsync(cancellationToken);
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