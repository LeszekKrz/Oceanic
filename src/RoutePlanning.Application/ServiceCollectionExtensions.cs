using Microsoft.Extensions.DependencyInjection;
using Netcompany.Net.Cqs;
using Netcompany.Net.DomainDrivenDesign;
using Netcompany.Net.Events;
using RoutePlanning.Domain;

namespace RoutePlanning.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRoutePlanningApplication(this IServiceCollection services)
    {
        services.AddEventHandling();

        services
            .AddDomainDrivenDesign()
            .WithRoutePlanning();

        services.AddCqs();

        return services;
    }
}
