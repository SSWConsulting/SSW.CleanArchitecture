namespace SSW.CleanArchitecture.Application.Features.Heroes.Commands.UpdateHero;

public class UpdateHeroPowerDto
{
    public required string Name { get; set; }
    public required int PowerLevel { get; set; }
}