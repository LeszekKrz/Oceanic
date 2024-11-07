using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Parcel;
public class Parcel : AggregateRoot<Parcel>
{
    public Parcel(Guid bookingReference, float weight, float height, float width)
    {
        BookingReference = bookingReference;
        Weight = weight;
        Height = height;
        Width = width;
    }

    public Guid BookingReference { get; set; }
    public float Weight { get; set; }
    public float Height { get; set; }
    public float Width { get; set; }
}
