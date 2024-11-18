using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.Common.Interfaces;

namespace SSW.CleanArchitecture.Application.UseCases.EntityNames.Queries.QueryName;

public record QueryNameQuery : IRequest<ErrorOr<EntityNameDto>>;

public record EntityNameDto(/* Add properties here */);

internal sealed class QueryNameQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<QueryNameQuery, ErrorOr<EntityNameDto>>
{
    public async Task<ErrorOr<EntityNameDto>> Handle(
        QueryNameQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}