using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Booking;
using RoutePlanning.Domain.Users;

namespace RoutePlanning.Application.Bookings.Queries.GetAllBookings;
public sealed record GetAllBookingsQuery(AuthenticatedUser user) : IQuery<IReadOnlyList<Booking>>
{
}
