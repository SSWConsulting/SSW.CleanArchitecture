using Microsoft.Extensions.Logging;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.Features.Teams.Events;

public class TeamPowerLevelUpdatedEventHandler(
    IApplicationDbContext dbContext,
    ILogger<TeamPowerLevelUpdatedEventHandler> logger)
    : INotificationHandler<TeamPowerLevelUpdatedEvent>
{
    public async Task Handle(TeamPowerLevelUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var team = await dbContext.Teams.FirstAsync(t => t.Id == notification.Team.Id, cancellationToken);

        team.SetUpdated(DateTimeOffset.Now, "Admin");

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}