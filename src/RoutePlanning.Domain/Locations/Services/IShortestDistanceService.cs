using Netcompany.Net.DomainDrivenDesign.Services;

namespace RoutePlanning.Domain.Locations.Services;

public interface IShortestDistanceService : IDomainService
{
    List<(string, string)> CalculateFastestPathPlane(Location source, Location target);
}
