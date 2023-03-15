using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Application.IntegrationTests.TestHelpers;

internal class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>
{
    public DatabaseContainer Database { get; }
    
    public IntegrationTestWebApplicationFactory()
    {
        Database = new DatabaseContainer();
    }

    protected override void ConfigureWebHost(IWebHostBuilder webHostBuilder)
    {
        webHostBuilder.ConfigureTestServices((services) => 
        {
            services
                .RemoveAll<DbContextOptions<ApplicationDbContext>>()
                .RemoveAll<ApplicationDbContext>()
                .AddDbContext<ApplicationDbContext>((_, options) =>
                    options.UseSqlServer(
                        Database.ConnectionString, 
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        });
    }
}