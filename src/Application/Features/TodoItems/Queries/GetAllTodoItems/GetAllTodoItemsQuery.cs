using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.Common.Interfaces;

namespace SSW.CleanArchitecture.Application.Features.TodoItems.Queries.GetAllTodoItems;

public record GetAllTodoItemsQuery : IRequest<IReadOnlyList<TodoItemDto>>;

public class GetAllTodoItemsQueryHandler : IRequestHandler<GetAllTodoItemsQuery, IReadOnlyList<TodoItemDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public GetAllTodoItemsQueryHandler(
        IMapper mapper,
        IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<TodoItemDto>> Handle(
        GetAllTodoItemsQuery request,
        CancellationToken cancellationToken)
    {
        return await _dbContext.TodoItems
            .ProjectTo<TodoItemDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}