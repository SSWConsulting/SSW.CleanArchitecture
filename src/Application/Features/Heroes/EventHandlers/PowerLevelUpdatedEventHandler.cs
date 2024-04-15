using Microsoft.Extensions.Logging;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Heroes;

namespace SSW.CleanArchitecture.Application.Features.Heroes.EventHandlers;

public class PowerLevelUpdatedEventHandler(ILogger<PowerLevelUpdatedEventHandler> logger, IApplicationDbContext dbContext)
    : INotificationHandler<PowerLevelUpdatedEvent>
{
    public Task Handle(PowerLevelUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("PowerLevelUpdatedEventHandler: {Name} power level was updated by {PowerDifference}", notification.Name, notification.PowerDifference);

        return Task.CompletedTask;
    }
}