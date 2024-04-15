using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Heroes;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.Features.Heroes.Commands.CreateHero;

public sealed record CreateHeroCommand(
    string Name,
    string Alias,
    Guid TeamId,
    IEnumerable<CreateHeroPowerDto> Powers) : IRequest<Guid>;

// ReSharper disable once UnusedType.Global
public sealed class CreateHeroCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<CreateHeroCommand, Guid>
{
    public async Task<Guid> Handle(CreateHeroCommand request, CancellationToken cancellationToken)
    {
        var hero = Hero.Create(request.Name, request.Alias);
        foreach (var heroPowerModel in request.Powers)
        {
            hero.AddPower(new Power(heroPowerModel.Name, heroPowerModel.PowerLevel));
        }

        Team? team = await dbContext.Teams.FirstOrDefaultAsync(t => t.Id == new TeamId(request.TeamId), cancellationToken);
        if (team is null)
        {
            throw new ArgumentException("Invalid team id", nameof(request.TeamId));
        }
        
        team.AddHero(hero);

        await dbContext.Heroes.AddAsync(hero, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return hero.Id.Value;
    }
}