using Application.Features.TodoItems.Specifications;
using Ardalis.Specification;
using Domain.Entities;
using Domain.Events;

namespace Application.Features.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand(string? Title) : IRequest<Guid>;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    private readonly IRepositoryBase<TodoItem> _repository;

    public CreateTodoItemCommandValidator(
        IRepositoryBase<TodoItem> repository)
    {
        _repository = repository;

        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle).WithMessage("'{PropertyName}' must be unique");
    }

    private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        var spec = new TodoItemByTitleSpec(title);
        var exists = await _repository.AnyAsync(spec, cancellationToken);
        return !exists;
    }
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryBase<TodoItem> _repository;

    public CreateTodoItemCommandHandler(
        IMapper mapper,
        IRepositoryBase<TodoItem> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreateTodoItemCommand request,
        CancellationToken cancellationToken)
    {
        var todoItem = _mapper.Map<TodoItem>(request);

        todoItem.AddDomainEvent(new TodoItemCreatedEvent(todoItem));

        await _repository.AddAsync(todoItem, cancellationToken);

        return todoItem.Id.Value;
    }
}