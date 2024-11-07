namespace RoutePlanning.Domain.Booking;
public class Booking
{
    public Booking(Guid reference, string origin, string destination)
    {
        Reference = reference;
        Origin = origin;
        Destination = destination;
    }
    public Guid Reference { get; }
    public string Origin { get; }
    public string Destination { get; }
}
