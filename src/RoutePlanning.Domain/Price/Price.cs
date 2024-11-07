using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Price;
public class Price : AggregateRoot<Price>
{
    public Price(string parcelType, string parcelWeight, int price)
    {
        ParcelType = parcelType;
        ParcelWeight = parcelWeight;
        Cost = price;
    }

    public string ParcelType { get; set; }

    public string ParcelWeight { get; set; }

    public float Cost {get; set; }

    protected Price(string parcelType, string parcelWeight) {
        ParcelType = parcelType;
        ParcelWeight = parcelWeight;
    }
}
