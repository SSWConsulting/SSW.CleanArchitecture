using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.Common.Interfaces;

namespace SSW.CleanArchitecture.Application.UseCases.EntityNames.Queries.QueryName;

public record QueryNameQuery : IRequest<ErrorOr<EntityNameDto>>;

public class QueryNameQueryHandler : IRequestHandler<QueryNameQuery, ErrorOr<EntityNameDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public QueryNameQueryHandler(
        IMapper mapper,
        IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<EntityNameDto>> Handle(
        QueryNameQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}