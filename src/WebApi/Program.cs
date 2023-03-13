using Application;
using Application.Common.Interfaces;
using Infrastructure;
using WebApi;
using WebApi.Features;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapTodoItemEndpoints();

app.Run();