using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Booking;
using RoutePlanning.Domain.Users;

namespace RoutePlanning.Application.Bookings.Queries.GetAllBookingForUser;

public sealed record GetUserBookingsQuery(AuthenticatedUser user) : IQuery<IReadOnlyList<Booking>>
{

}
