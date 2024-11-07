using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Locations;

namespace RoutePlanning.Application.Locations.Queries.Distance;

public sealed record DistanceQuery(string from, string to, float weight, string size, string type, bool fastest, bool usesCar, bool usesShip) : IQuery<RouteDTO>;
