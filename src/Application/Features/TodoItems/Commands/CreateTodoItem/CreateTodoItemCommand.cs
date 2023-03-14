using Application.Common.Interfaces;

namespace Application.Features.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand(string? Title) : IRequest<Guid>;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(200);
    }
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;
    private readonly IApplicationDbContext _dbContext;
    public CreateTodoItemCommandHandler(IMapper mapper, IPublisher publisher, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _publisher = publisher;
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todoItem = _mapper.Map<Domain.Entities.TodoItem>(request);

        _dbContext.TodoItems.Add(todoItem);

        await _dbContext.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new TodoItemCreatedEvent(todoItem), cancellationToken);

        return todoItem.Id.Value;
    }
}