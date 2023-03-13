using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.TodoItems.Queries.GetAllTodoItems;

public record GetAllTodoItemsQuery : IRequest<IReadOnlyList<TodoItemDto>>;

public class GetAllTodoItemsQueryHandler : IRequestHandler<GetAllTodoItemsQuery, IReadOnlyList<TodoItemDto>>
{
    private readonly IMapper _mapper;
    
    public GetAllTodoItemsQueryHandler(IMapper mapper) => _mapper = mapper;

    public async Task<IReadOnlyList<TodoItemDto>> Handle(GetAllTodoItemsQuery request, CancellationToken cancellationToken)
    {
        // _context.TodoItems.ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<TodoItemDto>>(Array.Empty<TodoItem>());
    }
}