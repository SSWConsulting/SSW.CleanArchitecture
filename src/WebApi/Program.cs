using Scalar.AspNetCore;
using SSW.CleanArchitecture.Application;
using SSW.CleanArchitecture.Infrastructure;
using SSW.CleanArchitecture.WebApi;
using SSW.CleanArchitecture.WebApi.Features;
using SSW.CleanArchitecture.WebApi.Filters;
using SSW.CleanArchitecture.WebApi.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

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

app.MapOpenApi();
app.MapScalarApiReference(options => options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient));
app.UseHealthChecks();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseDefaultExceptionHandler();
app.MapHeroEndpoints();
app.MapTeamEndpoints();

app.MapDefaultEndpoints();

app.Run();