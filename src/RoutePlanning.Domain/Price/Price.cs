using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Price;
public class Price : AggregateRoot<Price>
{
    public Price(string parcelType, int price)
    {
        ParcelType = parcelType;
        Cost = price;
    }

    public string ParcelType { get; set; }

    public float Cost {get; set; }

    protected Price(string parcelType) {
        ParcelType = parcelType;
    }
}
