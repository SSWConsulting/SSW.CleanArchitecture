using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Heroes;
using System.Text.Json.Serialization;

namespace SSW.CleanArchitecture.Application.UseCases.Heroes.Commands.UpdateHero;

public sealed record UpdateHeroCommand(
    string Name,
    string Alias,
    IEnumerable<UpdateHeroPowerDto> Powers) : IRequest<ErrorOr<Guid>>
{
    [JsonIgnore]
    public Guid HeroId { get; set; }
}

public record UpdateHeroPowerDto(string Name, int PowerLevel);

internal sealed class UpdateHeroCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<UpdateHeroCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(UpdateHeroCommand request, CancellationToken cancellationToken)
    {
        var heroId = HeroId.From(request.HeroId);
        var hero = await dbContext.Heroes
            .Include(h => h.Powers)
            .FirstOrDefaultAsync(h => h.Id == heroId, cancellationToken);

        if (hero is null)
            return HeroErrors.NotFound;

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