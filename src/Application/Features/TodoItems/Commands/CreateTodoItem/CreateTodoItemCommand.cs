using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Application.Features.TodoItems.Specifications;
using SSW.CleanArchitecture.Domain.Entities;

namespace SSW.CleanArchitecture.Application.Features.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand(string? Title) : IRequest<Guid>;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateTodoItemCommandValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle).WithMessage("'{PropertyName}' must be unique");
    }

    private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        var spec = new TodoItemByTitleSpec(title);

        var exists = await _dbContext.TodoItems
            .WithSpecification(spec)
            .AnyAsync(cancellationToken: cancellationToken);

        return !exists;
    }
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public CreateTodoItemCommandHandler(
        IMapper mapper,
        IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(
        CreateTodoItemCommand request,
        CancellationToken cancellationToken)
    {
        var todoItem = _mapper.Map<TodoItem>(request);

        await _dbContext.TodoItems.AddAsync(todoItem, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return todoItem.Id.Value;
    }
}