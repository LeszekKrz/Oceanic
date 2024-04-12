using Microsoft.EntityFrameworkCore;
using Netcompany.Net.Cqs.Commands;
using Netcompany.Net.DomainDrivenDesign.Services;
using RoutePlanning.Domain.Locations;

namespace RoutePlanning.Application.Locations.Commands.CreateTwoWayConnection;

public sealed class CreateTwoWayConnectionCommandHandler : ICommandHandler<CreateTwoWayConnectionCommand>
{
    private readonly IRepository<Location> locations;

    public CreateTwoWayConnectionCommandHandler(IRepository<Location> locations)
    {
        this.locations = locations;
    }

    public async Task Handle(CreateTwoWayConnectionCommand command, CancellationToken cancellationToken)
    {
        var locationA = await locations.FirstAsync(l => l.Id == command.LocationAId, cancellationToken);
        var locationB = await locations.FirstAsync(l => l.Id == command.LocationBId, cancellationToken);

        locationA.AddConnection(locationB, command.Distance);
        locationB.AddConnection(locationA, command.Distance);
    }
}
