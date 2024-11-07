using System.Runtime.InteropServices;
using Netcompany.Net.DomainDrivenDesign.Models;
using RoutePlanning.Domain.Users;

namespace RoutePlanning.Domain.Booking;
public sealed class Booking : AggregateRoot<Booking>
{
    public Booking(Guid reference, string origin, string destination, float price, float revenue, int time)
    {
        Reference = reference;
        Origin = origin;
        Destination = destination;
        Price = price;
        Revenue = revenue;
        Time = time;
    }
    public Guid Reference { get; }
    public string Origin { get; }
    public string Destination { get; }
    public float Price { get; set; }
    public int Time { get; }
    public float Revenue { get; set; }
}
