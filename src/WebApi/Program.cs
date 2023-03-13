using WebApi.Features;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapTodoItemEndpoints();

app.Run();