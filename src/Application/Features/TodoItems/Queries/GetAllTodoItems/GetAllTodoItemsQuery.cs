using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.Common.Interfaces;

namespace SSW.CleanArchitecture.Application.Features.TodoItems.Queries.GetAllTodoItems;

public record GetAllTodoItemsQuery : IRequest<IReadOnlyList<TodoItemDto>>;

public class GetAllTodoItemsQueryHandler(
    IMapper mapper,
    IApplicationDbContext dbContext) : IRequestHandler<GetAllTodoItemsQuery, IReadOnlyList<TodoItemDto>>
{
    public async Task<IReadOnlyList<TodoItemDto>> Handle(
        GetAllTodoItemsQuery request,
        CancellationToken cancellationToken)
    {
        return await dbContext.TodoItems
            .ProjectTo<TodoItemDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}