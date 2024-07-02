using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Heroes;

namespace SSW.CleanArchitecture.Application.Features.Heroes.Commands.UpdateHero;

public sealed record UpdateHeroCommand(
    HeroId HeroId,
    string Name,
    string Alias,
    IEnumerable<UpdateHeroPowerDto> Powers) : IRequest<Result<Guid>>;

// ReSharper disable once UnusedType.Global
public sealed class UpdateHeroCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<UpdateHeroCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateHeroCommand request, CancellationToken cancellationToken)
    {
        var hero = await dbContext.Heroes
            .Include(h => h.Powers)
            .FirstOrDefaultAsync(h => h.Id == request.HeroId, cancellationToken);

        if (hero is null)
            return Result.NotFound($"Hero {request.HeroId.Value}");

        hero.UpdateName(request.Name);
        hero.UpdateAlias(request.Alias);
        var powers = request.Powers.Select(p => new Power(p.Name, p.PowerLevel));
        hero.UpdatePowers(powers);

        await dbContext.SaveChangesAsync(cancellationToken);

        return hero.Id.Value;
    }
}

public class UpdateHeroCommandValidator : AbstractValidator<UpdateHeroCommand>
{
    public UpdateHeroCommandValidator()
    {
        RuleFor(v => v.HeroId)
            .NotEmpty();

        RuleFor(v => v.Name)
            .NotEmpty();

        RuleFor(v => v.Alias)
            .NotEmpty();
    }
}