﻿using Microsoft.EntityFrameworkCore;
using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Application.Locations.Commands.GetRoute;
using RoutePlanning.Domain.Locations;
using RoutePlanning.Domain.Locations.Services;
using RoutePlanning.Infrastructure.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        var priceBracket = "";
        if (request.weight <= 1)
        {
            priceBracket = "<1kg";
        }
        else if (request.weight <= 5)
        {
            priceBracket = "<5kg";
        }
        else
        {
            priceBracket = ">5kg";
        }
        var price = 50.0f; // default
        var prices = await priceRepository.getAllPrices(cancellationToken);
        var pricePossible = prices.Where(p => p.ParcelWeight == priceBracket && p.ParcelType == request.size).FirstOrDefault();
        if (pricePossible != null)
        {
            price = pricePossible.Cost;
        }

        var priceMult = 1.0f;
        switch (request.type)
        {
            case "Weapons":
                priceMult = 2.0f;
                break;
            case "Cautious Parcels":
                priceMult = 1.75f;
                break;
            case "Refrigerated Goods":
                priceMult = 1.1f;
                break;
        }
        price *= priceMult;
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
