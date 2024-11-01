namespace SSW.CleanArchitecture.Application.UseCases.Heroes.Commands.CreateHero;

public class CreateHeroPowerDto
{
    public required string Name { get; set; }
    public required int PowerLevel { get; set; }
}