using Microsoft.EntityFrameworkCore;
using Netcompany.Net.Cqs.Commands;
using Netcompany.Net.Cqs.Queries;
using Netcompany.Net.DomainDrivenDesign.Services;
using RoutePlanning.Domain.Locations;

namespace RoutePlanning.Application.Locations.Commands.GetRoute;

public sealed class GetRouteRequestHandler : IQueryHandler<GetRouteRequest, RouteDTO>
{
    //private readonly IRepository<Location> locations;

    public GetRouteRequestHandler()
    {
        //this.locations = locations;
    }

    public Task<RouteDTO> Handle(GetRouteRequest command, CancellationToken cancellationToken)
    {
        return Task.FromResult(new RouteDTO(command.From, command.To, 8, 40));
    }
}
