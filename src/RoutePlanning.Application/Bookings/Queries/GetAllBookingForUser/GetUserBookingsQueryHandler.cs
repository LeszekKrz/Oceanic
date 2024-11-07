using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Booking;
using RoutePlanning.Infrastructure.Repositories;

namespace RoutePlanning.Application.Bookings.Queries.GetAllBookingForUser;
public sealed class GetUserBookingsQueryHandler : IQueryHandler<GetUserBookingsQuery, IReadOnlyList<Booking>>
{
    public readonly IBookingRepository bookingRepository;
    public readonly IUserRepository userRepository;

    public GetUserBookingsQueryHandler(IBookingRepository bookingRepository, IUserRepository userRepository)
    {
        this.bookingRepository = bookingRepository;
        this.userRepository = userRepository;
    }
    public Task<IReadOnlyList<Booking>> Handle(GetUserBookingsQuery request, CancellationToken cancellationToken)
    {
        var user = userRepository.getUserByNameOfAuthenticatedUser(request.user.Username, cancellationToken);
        var bookings = bookingRepository.GetAllBookingByUser(user.Id, cancellationToken);

        return bookings;
    }
}
