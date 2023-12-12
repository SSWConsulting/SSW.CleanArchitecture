using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.TodoItems;

namespace SSW.CleanArchitecture.Application.Features.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand(string Title) : IRequest<Guid>;

// ReSharper disable once UnusedType.Global
public class CreateTodoItemCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<CreateTodoItemCommand, Guid>
{
    public async Task<Guid> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todoItem = TodoItem.Create(request.Title!);

        await dbContext.TodoItems.AddAsync(todoItem, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return todoItem.Id.Value;
    }
}