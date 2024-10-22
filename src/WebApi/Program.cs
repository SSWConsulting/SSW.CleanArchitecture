using SSW.CleanArchitecture.Application;
using SSW.CleanArchitecture.Infrastructure;
using SSW.CleanArchitecture.Infrastructure.Persistence;
using SSW.CleanArchitecture.WebApi;
using SSW.CleanArchitecture.WebApi.Features;
using SSW.CleanArchitecture.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApi(builder.Configuration);
builder.Services.AddApplication();
builder.AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseOpenApi();
app.UseSwaggerUi();

app.UseRouting();

app.UseDefaultExceptionHandler();
app.MapHeroEndpoints();
app.MapTeamEndpoints();

app.Run();

public partial class Program { }