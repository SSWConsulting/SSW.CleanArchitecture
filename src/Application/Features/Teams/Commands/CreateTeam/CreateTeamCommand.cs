using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.Features.Teams.Commands.CreateTeam;

public sealed record CreateTeamCommand(string Name) : IRequest;

// ReSharper disable once UnusedType.Global
public sealed class CreateTeamCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<CreateTeamCommand>
{
    public async Task Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = Team.Create(request.Name);

        await dbContext.Teams.AddAsync(team, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
{
    public CreateTeamCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty();
    }
}