using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Heroes;

namespace SSW.CleanArchitecture.Application.Features.Heroes.Commands.CreateHero;

public sealed record CreateHeroCommand(
    string Name,
    string Alias,
    IEnumerable<CreateHeroPowerDto> Powers) : IRequest<Result<Guid>>;

// ReSharper disable once UnusedType.Global
public sealed class CreateHeroCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<CreateHeroCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateHeroCommand request, CancellationToken cancellationToken)
    {
        var hero = Hero.Create(request.Name, request.Alias);
        var powers  = request.Powers.Select(p => new Power(p.Name, p.PowerLevel));
        hero.UpdatePowers(powers);

        await dbContext.Heroes.AddAsync(hero, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return hero.Id.Value;
    }
}

public class CreateHeroCommandValidator : AbstractValidator<CreateHeroCommand>
{
    public CreateHeroCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty();

        RuleFor(v => v.Alias)
            .NotEmpty();
    }
}