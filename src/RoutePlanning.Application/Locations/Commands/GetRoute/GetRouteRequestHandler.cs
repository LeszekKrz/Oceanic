using Microsoft.EntityFrameworkCore;
using Netcompany.Net.Cqs.Commands;
using Netcompany.Net.Cqs.Queries;
using Netcompany.Net.DomainDrivenDesign.Services;
using RoutePlanning.Domain.Locations;
using RoutePlanning.Infrastructure.Repositories;

namespace RoutePlanning.Application.Locations.Commands.GetRoute;

public sealed class GetRouteRequestHandler : IQueryHandler<GetRouteRequest, RouteInfoDTO>
{
    private readonly IRepository<Location> locations;
    private readonly IPriceRepository priceRepository;

    public GetRouteRequestHandler(IRepository<Location> locations, IPriceRepository priceRepository)
    {
        this.locations = locations;
        this.priceRepository = priceRepository;
    }

    public async Task<RouteInfoDTO> Handle(GetRouteRequest command, CancellationToken cancellationToken)
    {
        var from = locations.Where(l => l.Name == command.From && l.airReachable == true).FirstOrDefault();
        if (from is not null)
        {
            var to = locations.Where(l => l.Name == command.To && l.airReachable == true).FirstOrDefault();
            if (to is not null)
            {
                var prices = await priceRepository.getAllPrices(cancellationToken);
                var price = prices.Where(p => p.ParcelWeight == command.weight && p.ParcelType == command.size).FirstOrDefault();
                return new RouteInfoDTO(true, command.From, command.To, 8, price?.Cost ?? 50);
            }
        }
        return new RouteInfoDTO(false, "", "", 0, 0);
    }
}
