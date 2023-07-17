using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.TodoItems;

namespace SSW.CleanArchitecture.Application.Features.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand(string Title) : IRequest<Guid>;

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateTodoItemCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todoItem = TodoItem.Create(request.Title!);

        await _dbContext.TodoItems.AddAsync(todoItem, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return todoItem.Id.Value;
    }
}
