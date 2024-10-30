namespace SSW.CleanArchitecture.Application.UseCases.Heroes.Queries.GetAllHeroes;

public class HeroDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Alias { get; set; }
    public int PowerLevel { get; set; }
    public IEnumerable<HeroPowerDto> Powers { get; set; } = [];
}

public class HeroPowerDto
{
    public required string Name { get; set; }
    public int PowerLevel { get; set; }
}