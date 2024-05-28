using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.Features.Teams.Queries.GetAllTeams;

public record GetAllTeamsQuery : IRequest<IReadOnlyList<TeamDto>>;

public sealed class GetAllTeamsQueryHandler(
    IMapper mapper,
    IApplicationDbContext dbContext) : IRequestHandler<GetAllTeamsQuery, IReadOnlyList<TeamDto>>
{
    public async Task<IReadOnlyList<TeamDto>> Handle(
        GetAllTeamsQuery request,
        CancellationToken cancellationToken)
    {
        return await dbContext.Teams
            .ProjectTo<TeamDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

public class TeamDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
}

public class GetAllTeamsMapping : Profile
{
    public GetAllTeamsMapping()
    {
        CreateMap<Team, TeamDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
    }
}