namespace Application.Features.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemMapping : Profile
{
    public CreateTodoItemMapping()
    {
        CreateMap<CreateTodoItemCommand, Domain.Entities.TodoItem>();
    }
}