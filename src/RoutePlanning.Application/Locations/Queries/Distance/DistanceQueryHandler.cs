using Microsoft.EntityFrameworkCore;
using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Locations;
using RoutePlanning.Domain.Locations.Services;

namespace RoutePlanning.Application.Locations.Queries.Distance;

public sealed class DistanceQueryhandler : IQueryHandler<DistanceQuery, int>
{
    private readonly IQueryable<Location> locations;
    private readonly IShortestDistanceService shortestDistanceService;

    public DistanceQueryhandler(IQueryable<Location> locations, IShortestDistanceService shortestDistanceService)
    {
        this.locations = locations;
        this.shortestDistanceService = shortestDistanceService;
    }

    public async Task<int> Handle(DistanceQuery request, CancellationToken cancellationToken)
    {
        var source = await locations.FirstAsync(l => l.Id == request.SourceId, cancellationToken);
        var destination = await locations.FirstAsync(l => l.Id == request.DestinationId, cancellationToken);

        var distance = shortestDistanceService.CalculateShortestDistance(source, destination);

        return distance;
    }
}
