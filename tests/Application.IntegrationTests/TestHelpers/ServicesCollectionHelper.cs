using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests.TestHelpers;

public static class ServicesCollectionHelper
{
    public static IServiceCollection Remove<TService>(this IServiceCollection services)
    {
        var serviceDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(TService));

        if (serviceDescriptor != null)
        {
            services.Remove(serviceDescriptor);
        }

        return services;
    }
    
}