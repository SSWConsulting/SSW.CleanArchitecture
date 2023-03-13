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

    public CreateTodoItemCommandHandler(IMapper mapper, IPublisher publisher)
    {
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<Guid> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todoItem = _mapper.Map<Domain.Entities.TodoItem>(request);
        // ?????????????????????????????
        
        // _context.TodoItems.Add(todoItem);
        
        // await _context.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new TodoItemCreatedEvent(todoItem), cancellationToken);
        
        return Guid.NewGuid(); // TODOL: return todoItem.Id;
    }
}