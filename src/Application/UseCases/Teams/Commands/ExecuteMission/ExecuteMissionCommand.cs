using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Teams;
using System.Text.Json.Serialization;

namespace SSW.CleanArchitecture.Application.UseCases.Teams.Commands.ExecuteMission;

public sealed record ExecuteMissionCommand(string Description) : IRequest<ErrorOr<Success>>
{
    [JsonIgnore] public Guid TeamId { get; set; }
}

internal sealed class ExecuteMissionCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<ExecuteMissionCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(ExecuteMissionCommand request, CancellationToken cancellationToken)
    {
        var teamId = TeamId.From(request.TeamId);
        var team = dbContext.Teams
            .WithSpecification(TeamSpec.ById(teamId))
            .FirstOrDefault();

        if (team is null)
            return TeamErrors.NotFound;

        var result = team.ExecuteMission(request.Description);
        if (result.IsError)
            return result;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new Success();
    }
}

internal sealed class ExecuteMissionCommandValidator : AbstractValidator<ExecuteMissionCommand>
{
    public ExecuteMissionCommandValidator()
    {
        RuleFor(v => v.TeamId)
            .NotEmpty();

        RuleFor(v => v.Description)
            .NotEmpty();
    }
}