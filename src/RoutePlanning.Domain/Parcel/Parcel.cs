using System.Security.Cryptography.X509Certificates;

namespace RoutePlanning.Domain.Parcel;
public class Parcel
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
