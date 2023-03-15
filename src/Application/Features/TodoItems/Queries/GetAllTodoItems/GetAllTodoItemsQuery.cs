using Application.Features.TodoItems.Specifications;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Features.TodoItems.Queries.GetAllTodoItems;

public record GetAllTodoItemsQuery : IRequest<IReadOnlyList<TodoItemDto>>;

public class GetAllTodoItemsQueryHandler : IRequestHandler<GetAllTodoItemsQuery, IReadOnlyList<TodoItemDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadRepositoryBase<TodoItem> _repository;

    public GetAllTodoItemsQueryHandler(
        IMapper mapper,
        IReadRepositoryBase<TodoItem> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IReadOnlyList<TodoItemDto>> Handle(
        GetAllTodoItemsQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new AllTodoItemSpec();
        var items = await _repository.ListAsync(spec, cancellationToken);
        return items.Select(_mapper.Map<TodoItemDto>).ToList();
    }
}