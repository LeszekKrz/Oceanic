﻿using Microsoft.EntityFrameworkCore;

namespace RoutePlanning.Domain.Locations.Services;

public sealed class ShortestDistanceService : IShortestDistanceService
{
    private readonly IQueryable<Location> locations;

    public ShortestDistanceService(IQueryable<Location> locations)
    {
        this.locations = locations;
    }

    public List<(string, string)> CalculateFastestPathPlane(Location source, Location target)
    {
        return CalculateShortestDistance(source, target);
    }

    public List<(string, string)> CalculateShortestDistance(Location source, Location target)
    {
        var locations = this.locations.Include(l => l.Connections).ThenInclude(c => c.Destination);

        var path = CalculateShortestPath(locations, source, target);

        return path;
    }

    /// <summary>
    /// An implementation of the Dijkstra's shortest path algorithm
    /// </summary>
    private static List<(string, string)> CalculateShortestPath(IEnumerable<Location> locations, Location start, Location end)
    {
        var shortestConnections = CalculateShortestConnections(locations, start, end);

        var path = ConstructShortestPath(start, end, shortestConnections);

        var route = DecupleThePath(path);

        return route;
    }

    /// <summary>
    /// An implementation of the Dijkstra's algorithm that computes the shortest connections to all locations until the end location is reached
    /// </summary>
    private static Dictionary<Location, (Connection? SourceConnection, int Distance)> CalculateShortestConnections(IEnumerable<Location> locations, Location start, Location end)
    {
        var shortestConnections = new Dictionary<Location, (Connection? SourceConnection, int Distance)>();
        var unvisitedLocations = locations.ToHashSet();

        foreach (var location in unvisitedLocations)
        {
            shortestConnections[location] = (SourceConnection: null, Distance: int.MaxValue);
        }

        shortestConnections[start] = (SourceConnection: null, Distance: 0);

        while (unvisitedLocations.Count > 0)
        {
            var location = unvisitedLocations.OrderBy(location => shortestConnections[location].Distance).First();

            if (location == end)
            {
                break;
            }

            foreach (var connection in location.Connections)
            {
                UpdateShortestConnections(shortestConnections, location, connection);
            }

            unvisitedLocations.Remove(location);
        }

        return shortestConnections;
    }

    private static void UpdateShortestConnections(Dictionary<Location, (Connection? SourceConnection, int Distance)> shortestConnections, Location location, Connection connection)
    {
        var distance = shortestConnections[location].Distance + 1;

        if (distance < shortestConnections[connection.Destination].Distance)
        {
            shortestConnections[connection.Destination] = (SourceConnection: connection, Distance: distance);
        }
    }

    /// <summary>
    /// The shortest path is constructed by backtracking the Dijkstra connection data from the end location
    /// </summary>
    private static IEnumerable<Connection> ConstructShortestPath(Location start, Location end, Dictionary<Location, (Connection? SourceConnection, int Distance)> sourceConnections)
    {
        var path = new List<Connection>();
        var location = end;

        while (location != start)
        {
            var shortestConnection = sourceConnections[location].SourceConnection!;
            path.Add(shortestConnection);
            location = shortestConnection.Source;
        }

        path.Reverse();

        return path;
    }

    private static List<(string, string)> DecupleThePath(IEnumerable<Connection> route)
    {
        var decupled = new List<(string, string)>();
        foreach (var connection in route)
        {
            decupled.Add(new(connection.Source.Name, connection.Destination.Name));
        }
        return decupled;
    }
}
