namespace SSW.CleanArchitecture.Application.UseCases.Teams.Queries.GetAllTeams;

public class TeamDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
}