using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Heroes;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.UseCases.Teams.Commands.AddHeroToTeam;

public sealed record AddHeroToTeamCommand(Guid TeamId, Guid HeroId) : IRequest<ErrorOr<Success>>;

internal sealed class AddHeroToTeamCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<AddHeroToTeamCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(AddHeroToTeamCommand request, CancellationToken cancellationToken)
    {
        var teamId = TeamId.From(request.TeamId);
        var heroId = HeroId.From(request.HeroId);

        var team = dbContext.Teams
            .WithSpecification(new TeamByIdSpec(teamId))
            .FirstOrDefault();

        if (team is null)
            return TeamErrors.NotFound;

        var hero = dbContext.Heroes
            .WithSpecification(new HeroByIdSpec(heroId))
            .FirstOrDefault();

        if (hero is null)
            return HeroErrors.NotFound;

        team.AddHero(hero);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new Success();
    }
}

internal sealed class AddHeroToTeamCommandValidator : AbstractValidator<AddHeroToTeamCommand>
{
    public AddHeroToTeamCommandValidator()
    {
        RuleFor(v => v.HeroId)
            .NotEmpty();

        RuleFor(v => v.TeamId)
            .NotEmpty();
    }
}