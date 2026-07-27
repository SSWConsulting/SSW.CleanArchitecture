using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.Common.Interfaces;

namespace SSW.CleanArchitecture.Application.UseCases.EntityNames.Queries.QueryName;

public record QueryNameQuery : IRequest<ErrorOr<EntityNameDto>>;

public record EntityNameDto(/* Add properties here */);

// dbContext is injected ready for the query you are about to write. Delete the pragmas once Handle reads it.
#pragma warning disable CS9113 // Parameter is unread
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
#pragma warning restore CS9113