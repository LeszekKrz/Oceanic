using Microsoft.EntityFrameworkCore;
using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Locations;
using RoutePlanning.Domain.Locations.Services;
using RoutePlanning.Infrastructure.Repositories;

namespace RoutePlanning.Application.Locations.Queries.Distance;

public sealed class DistanceQueryhandler : IQueryHandler<DistanceQuery, RouteDTO>
{
    private readonly IQueryable<Location> locations;
    private readonly IShortestDistanceService shortestDistanceService;
    private readonly IPriceRepository priceRepository;

    public DistanceQueryhandler(IQueryable<Location> locations, IShortestDistanceService shortestDistanceService, IPriceRepository priceRepository)
    {
        this.locations = locations;
        this.shortestDistanceService = shortestDistanceService;
        this.priceRepository = priceRepository;
    }

    public async Task<RouteDTO> Handle(DistanceQuery request, CancellationToken cancellationToken)
    {
        var source = await locations.FirstAsync(l => l.Name == request.from, cancellationToken);
        var destination = await locations.FirstAsync(l => l.Name == request.to, cancellationToken);

        var route = shortestDistanceService.CalculateFastestPathPlane(source, destination);

        // get price
        var price = 10.0f;
        // get time
        var time = 8;

        var result = CreateDTOFromRoute(route, time, price);

        return result;
    }

    private static RouteDTO CreateDTOFromRoute(List<(string From, string To)> routes, int timePerSegment, float pricePerSegment)
    {
        var totalTime = 0;
        var totalPrice = 0.0f;

        foreach (var route in routes)
        {
            totalTime += timePerSegment;
            totalPrice += pricePerSegment;
        }

        var segmentList = routes.Select<(string From, string To), (string From, string To, int time, float price)>
            (route => new(route.From, route.To, timePerSegment, pricePerSegment)).ToList();

        return new RouteDTO(totalTime, totalPrice, segmentList);
    }
}
