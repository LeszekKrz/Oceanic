using System.Diagnostics;
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations;

[DebuggerDisplay("{Name}")]
public sealed class Location : AggregateRoot<Location>
{
    public Location(string name)
    {
        Name = name;
        airReachable = true;
    }

    public string Name { get; set; }

    public bool airReachable {  get; set; }

    private readonly List<Connection> connections = [];

    public IReadOnlyCollection<Connection> Connections => connections.AsReadOnly();

    public Connection AddConnection(Location destination, int distance)
    {
        Connection connection = new(this, destination, distance);

        connections.Add(connection);

        return connection;
    }
}
