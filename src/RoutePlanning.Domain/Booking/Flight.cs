using System.Security.Cryptography.X509Certificates;
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Booking;
public sealed class Flight : AggregateRoot<Flight>
{
    public Flight(Guid bookingReference, string origin, string destination, int time)
    {
        BookingReference = bookingReference;
        Origin = origin;
        Destination = destination;
        Time = time;
    }
    
    public Guid BookingReference {get;}
    public string Origin {get;}
    public string Destination {get;}
    public int Time {get;}
}
