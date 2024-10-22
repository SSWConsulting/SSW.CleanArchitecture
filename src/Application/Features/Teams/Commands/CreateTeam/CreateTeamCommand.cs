using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.Features.Teams.Commands.CreateTeam;

public sealed record CreateTeamCommand(string Name) : IRequest<Result>;

// ReSharper disable once UnusedType.Global
public sealed class CreateTeamCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<CreateTeamCommand, Result>
{
    public async Task<Result> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = Team.Create(request.Name);

        await dbContext.Teams.AddAsync(team, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
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