using Microsoft.Extensions.Logging;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Common.EventualConsistency;
using SSW.CleanArchitecture.Domain.Heroes;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.UseCases.Teams.Events;

internal sealed class PowerLevelUpdatedEventHandler(
    IApplicationDbContext dbContext,
    ILogger<PowerLevelUpdatedEventHandler> logger)
    : INotificationHandler<PowerLevelUpdatedEvent>
{
    public async Task Handle(PowerLevelUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("PowerLevelUpdatedEventHandler: {HeroName} power updated to {PowerLevel}",
            notification.Hero.Name, notification.Hero.PowerLevel);

        var hero = await dbContext.Heroes.FirstAsync(h => h.Id == notification.Hero.Id,
            cancellationToken: cancellationToken);

        if (hero.TeamId is null)
        {
            logger.LogInformation("Hero {HeroName} is not on a team - nothing to do", notification.Hero.Name);
            return;
        }

        var team = dbContext.Teams
            .WithSpecification(TeamSpec.ById(hero.TeamId.Value))
            .FirstOrDefault();

        if (team is null)
            throw new EventualConsistencyException(PowerLevelUpdatedEvent.TeamNotFound);

        team.ReCalculatePowerLevel();
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}