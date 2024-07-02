using Ardalis.Result;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Heroes;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.Features.Teams.Commands.AddHeroToTeam;

public sealed record AddHeroToTeamCommand(Guid TeamId, Guid HeroId) : IRequest<Result>;

// ReSharper disable once UnusedType.Global
public sealed class AddHeroToTeamCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<AddHeroToTeamCommand, Result>
{
    public async Task<Result> Handle(AddHeroToTeamCommand request, CancellationToken cancellationToken)
    {
        var teamId = new TeamId(request.TeamId);
        var heroId = new HeroId(request.HeroId);

        var team = dbContext.Teams
            .WithSpecification(new TeamByIdSpec(teamId))
            .FirstOrDefault();

        if (team is null)
        {
            return Result.NotFound($"Team {teamId.Value}");
        }

        var hero = dbContext.Heroes
            .WithSpecification(new HeroByIdSpec(heroId))
            .FirstOrDefault();

        if (hero is null)
        {
            return Result.NotFound($"Hero {heroId.Value}");
        }

        team.AddHero(hero);
        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}

public class AddHeroToTeamCommandValidator : AbstractValidator<AddHeroToTeamCommand>
{
    public AddHeroToTeamCommandValidator()
    {
        RuleFor(v => v.HeroId)
            .NotEmpty();

        RuleFor(v => v.TeamId)
            .NotEmpty();
    }
}