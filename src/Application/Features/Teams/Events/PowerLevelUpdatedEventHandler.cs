using Microsoft.Extensions.Logging;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Heroes;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.Features.Teams.Events;

public class PowerLevelUpdatedEventHandler(IApplicationDbContext dbContext, ILogger<PowerLevelUpdatedEventHandler> logger)
    : INotificationHandler<PowerLevelUpdatedEvent>
{
    public async Task Handle(PowerLevelUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("PowerLevelUpdatedEventHandler: {HeroName} power updated to {PowerLevel}",
            notification.HeroName, notification.HeroPowerLevel);

        if (notification.TeamId is null)
        {
            logger.LogInformation("Hero {HeroName} is not on a team", notification.HeroName);
            return;
        }

        var team = dbContext.Teams
            .WithSpecification(new TeamByIdSpec(notification.TeamId))
            .FirstOrDefault();

        if (team is null)
        {
            logger.LogError("Team {TeamId} not found", notification.TeamId);
            return;
        }

        team.ReCalculatePowerLevel();
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}