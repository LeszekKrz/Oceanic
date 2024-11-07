using Microsoft.EntityFrameworkCore;
using RoutePlanning.Domain.Booking;
using RoutePlanning.Domain.Users;
using RoutePlanning.Infrastructure.Database;

namespace RoutePlanning.Infrastructure.Repositories;
public class BookingRepository : IBookingRepository
{
    protected readonly RoutePlanningDatabaseContext _context;
    public IQueryable<Booking> Queryable => GetDbSetWithIncludes();

    protected virtual IQueryable<Booking> GetDbSetWithIncludes()
    {
        return _context.Set<Booking>().AsNoTracking();
    }

    public BookingRepository(RoutePlanningDatabaseContext context)
    {
        _context = context;
    }
    public async Task<Booking?> getBookingByReference(Guid reference, CancellationToken cancellationToken)
    {
        var booking = await Queryable
            .Where(b => b.Reference.ToString() == reference.ToString())
            .SingleOrDefaultAsync(cancellationToken);
        return booking;
    }

    public void addBooking(Booking booking)
    {
        _context.Set<Booking>().Add(booking);
    }
}

public interface IBookingRepository
{
    public Task<Booking?> getBookingByReference(Guid reference, CancellationToken cancellationToken);
    public void addBooking(Booking booking);
}
