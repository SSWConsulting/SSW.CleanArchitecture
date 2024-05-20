using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Database;
using SSW.CleanArchitecture.Infrastructure;
using SSW.CleanArchitecture.Infrastructure.Persistence;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    var connection = context.Configuration.GetConnectionString("DefaultConnection");

    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection"), opt =>
        {
            opt.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);
        }));

    services.AddScoped<ApplicationDbContextInitializer>();
});

var app = builder.Build();
app.Start();

// Initialise and seed database
using var scope = app.Services.CreateScope();
var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
await initializer.InitializeAsync();
await initializer.SeedAsync();