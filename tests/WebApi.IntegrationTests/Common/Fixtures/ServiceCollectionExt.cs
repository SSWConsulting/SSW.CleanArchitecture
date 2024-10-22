// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Internal;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.DependencyInjection.Extensions;
// using Microsoft.IdentityModel.Protocols.OpenIdConnect;
// using SSW.CleanArchitecture.Infrastructure.Persistence.Interceptors;
// using System.Diagnostics;
//
// namespace WebApi.IntegrationTests.Common.Fixtures;
//
// internal static class ServiceCollectionExt
// {
//     /// <summary>
//     /// Replaces the DbContext with a new instance using the provided database container
//     /// </summary>
//     internal static IServiceCollection ReplaceDbContext<T>(
//         this IServiceCollection services,
//         DatabaseContainer databaseContainer) where T : DbContext
//     {
//         services
//             .RemoveAll<T>()
//             .RemoveAll<DbContextOptions<T>>()
//             .RemoveAll<DbContextOptions>()
//             .RemoveAll<DbContextPool<T>>()
//             .RemoveAll<IDbContextPool<T>>()
//             .RemoveAll<DbContextFactory<T>>()
//             .RemoveAll<IDbContextFactory<T>>()
//             .RemoveAll<ScopedDbContextLease<T>>()
//             .RemoveAll<IScopedDbContextLease<T>>()
//             ;
//
//         services
//             .AddDbContextPool<T>((_, options) =>
//             {
//                 options.UseSqlServer(databaseContainer.ConnectionString,
//                     b => b.MigrationsAssembly(typeof(T).Assembly.FullName));
//
//                 options.LogTo(m => Debug.WriteLine(m));
//                 options.EnableSensitiveDataLogging();
//
//                 var serviceProvider = services.BuildServiceProvider();
//
//                 options.AddInterceptors(
//                     serviceProvider.GetRequiredService<EntitySaveChangesInterceptor>(),
//                     serviceProvider.GetRequiredService<DispatchDomainEventsInterceptor>()
//                 );
//
//                 // options.UseExceptionProcessor();
//             });
//
//         return services;
//     }
// }