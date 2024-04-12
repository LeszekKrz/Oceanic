using Microsoft.Extensions.DependencyInjection;
using Netcompany.Net.DomainDrivenDesign;
using RoutePlanning.Domain.Locations.Services;

namespace RoutePlanning.Domain;

public static class DomainDrivenDesignBuilderExtensions
{
    public static IDomainDrivenDesignBuilder WithRoutePlanning(this IDomainDrivenDesignBuilder builder)
    {
        builder.Services.AddTransient<IShortestDistanceService, ShortestDistanceService>();
        return builder;
    }
}
