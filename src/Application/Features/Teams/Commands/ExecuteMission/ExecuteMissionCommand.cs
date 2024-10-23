using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Teams;
using System.Text.Json.Serialization;

namespace SSW.CleanArchitecture.Application.Features.Teams.Commands.ExecuteMission;

public sealed record ExecuteMissionCommand(string Description) : IRequest<ErrorOr<Success>>
{
    [JsonIgnore] public Guid TeamId { get; set; }
}

// ReSharper disable once UnusedType.Global
public sealed class ExecuteMissionCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<ExecuteMissionCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(ExecuteMissionCommand request, CancellationToken cancellationToken)
    {
        var teamId = new TeamId(request.TeamId);
        var team = dbContext.Teams
            .WithSpecification(new TeamByIdSpec(teamId))
            .FirstOrDefault();

        if (team is null)
            return TeamErrors.NotFound;

        team.ExecuteMission(request.Description);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new Success();
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