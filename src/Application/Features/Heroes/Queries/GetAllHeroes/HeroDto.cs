namespace SSW.CleanArchitecture.Application.Features.Heroes.Queries.GetAllHeroes;

public class HeroDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Alias { get; set; }
    public int PowerLevel { get; set; }
    public IEnumerable<HeroPowerDto> Powers { get; set; } = Enumerable.Empty<HeroPowerDto>();
}

public class HeroPowerDto
{
    public required string Name { get; set; }
    public int PowerLevel { get; set; }
}
