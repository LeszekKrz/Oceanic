using Microsoft.EntityFrameworkCore;
using Netcompany.Net.DomainDrivenDesign.Models;
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
    public async Task<Booking?> GetBookingByReference(Guid reference, CancellationToken cancellationToken)
    {
        var booking = await Queryable
            .Where(b => b.Reference.ToString() == reference.ToString())
            .SingleOrDefaultAsync(cancellationToken);
        return booking;
    }

    public void AddBooking(Booking booking, CancellationToken cancellationToken)
    {
        _context.Set<Booking>().Add(booking);
        _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Booking>> GetAllBookingByUser(int userId, CancellationToken cancellationToken)
    {
        var bookings = await Queryable
            .Where(b => b.BookedById == userId)
            .ToListAsync(cancellationToken);
        return bookings;
    }

    public async Task<IReadOnlyList<Booking>> GetAllBookings(CancellationToken cancellationToken)
    {
        var bookings = await Queryable.ToListAsync(cancellationToken);
        return bookings;
    }
}

public interface IBookingRepository
{
    public Task<Booking?> GetBookingByReference(Guid reference, CancellationToken cancellationToken);
    public void AddBooking(Booking booking, CancellationToken cancellationToken);

    public Task<IReadOnlyList<Booking>> GetAllBookingByUser(int userId, CancellationToken cancellationToken);

    public Task<IReadOnlyList<Booking>> GetAllBookings(CancellationToken cancellationToken);
}
