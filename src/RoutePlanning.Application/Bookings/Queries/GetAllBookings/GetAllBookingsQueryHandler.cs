using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Booking;
using RoutePlanning.Infrastructure.Repositories;

namespace RoutePlanning.Application.Bookings.Queries.GetAllBookings;
public sealed class GetAllBookingsQueryHandler : IQueryHandler<GetAllBookingsQuery, IReadOnlyList<Booking>>
{

    public readonly IBookingRepository bookingRepository;
    public readonly IUserRepository userRepository;

    public GetAllBookingsQueryHandler(IBookingRepository bookingRepository, IUserRepository userRepository)
    {
        this.bookingRepository = bookingRepository;
        this.userRepository = userRepository;
    }

    public async Task<IReadOnlyList<Booking>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.getUserByNameOfAuthenticatedUser(request.user.Username, cancellationToken);
        if (user == null)
        {
            return new List<Booking>();
        }
        if (!user.IsAdmin)
        {
            return new List<Booking>();
        }
        var bookings = bookingRepository.GetAllBookings(cancellationToken);

        return await bookings;

    }
}
