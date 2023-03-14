namespace Application.Features.TodoItems.Queries.GetAllTodoItems;

public class GetAllTodoItemsMapping : Profile
{
    public GetAllTodoItemsMapping()
    {
        CreateMap<Domain.Entities.TodoItem, TodoItemDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
    }
}