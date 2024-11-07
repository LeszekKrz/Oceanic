using System.ComponentModel.DataAnnotations.Schema;
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Email;
public sealed class Email : AggregateRoot<Email>
{
    public Email(Guid bookingReference, string recieverEmail)
    {
        BookingReference = bookingReference;
        RecieverEmail = recieverEmail;
        DateSent = DateTime.Now;
    }

    public Guid BookingReference { get; }

    public string RecieverEmail { get; }

    public DateTime DateSent { get; }
}
