namespace SSW.CleanArchitecture.Application.Features.TodoItems.Commands.CompleteTodoItem;

public class CompleteTodoItemCommandValidator : AbstractValidator<CompleteTodoItemCommand>
{
    public CompleteTodoItemCommandValidator()
    {
        RuleFor(p => p.TodoItemId)
            .NotEmpty();
    }
}