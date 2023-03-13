using Application.Features.TodoItems.Commands.CreateTodoItem;
using Application.Features.TodoItems.Queries.GetAllTodoItems;
using MediatR;

namespace WebApi.Features;

public static class TodoItemEndpoints
{
    public static void MapTodoItemEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("todoitems");
        
        group.MapGet("/", (ISender sender, CancellationToken ct) => sender.Send(new GetAllTodoItemsQuery(), ct));
        
        group.MapPost("/", (ISender sender, CreateTodoItemCommand command, CancellationToken ct) => sender.Send(command, ct));
    }
}