using Netcompany.Net.DomainDrivenDesign.Models;
using RoutePlanning.Domain.Users;

namespace RoutePlanning.Domain.Booking;
public sealed class Booking : AggregateRoot<Booking>
{
    public Booking(Guid reference, string origin, string destination, int bookedByUserId)
    {
        Reference = reference;
        Origin = origin;
        Destination = destination;
        Price = 0;
        BookedByUserId = bookedByUserId;
    }
    public Guid Reference { get; }
    public string Origin { get; }
    public string Destination { get; }

    public float Price { get; set; }

    public int BookedByUserId { get; set; }
}
