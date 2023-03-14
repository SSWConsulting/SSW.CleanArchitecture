using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand(string? Title) : IRequest<Guid>;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoItemCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle).WithMessage("'{PropertyName}' must be unique");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.TodoItems
            .AllAsync(l => l.Title != title, cancellationToken);
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