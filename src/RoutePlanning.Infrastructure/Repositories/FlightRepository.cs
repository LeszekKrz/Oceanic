using Microsoft.EntityFrameworkCore;
using RoutePlanning.Domain.Booking;
using RoutePlanning.Infrastructure.Database;

namespace RoutePlanning.Infrastructure.Repositories;
public class FlightRepository : IFlightRepository
{

    protected readonly RoutePlanningDatabaseContext _context;
    public IQueryable<Flight> Queryable => GetDbSetWithIncludes();

    protected virtual IQueryable<Flight> GetDbSetWithIncludes()
    {
        return _context.Set<Flight>().AsNoTracking();
    }

    public FlightRepository(RoutePlanningDatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<Flight>> getFlightsByReference(Guid reference, CancellationToken cancellationToken)
    {
        var flights = await Queryable
            .Where(f => f.BookingReference.ToString() == reference.ToString())
            .ToListAsync(cancellationToken);

        return flights;

    }

    public void addFlight(Flight flight)
    {
        _context.Set<Flight>().Add(flight);
    }
}

public interface IFlightRepository
{
    public Task<List<Flight>> getFlightsByReference(Guid reference, CancellationToken cancellationToken);

    public void addFlight(Flight flight);
}
