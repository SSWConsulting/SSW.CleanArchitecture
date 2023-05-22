using SSW.CleanArchitecture.Domain.Entities;

namespace SSW.CleanArchitecture.Application.Features.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemMapping : Profile
{
    public CreateTodoItemMapping()
    {
        CreateMap<CreateTodoItemCommand, TodoItem>();
    }
}