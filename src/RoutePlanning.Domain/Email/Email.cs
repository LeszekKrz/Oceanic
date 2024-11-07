namespace RoutePlanning.Domain.Email;
public class Email
{
    public Email(string bookingReference, string recieverEmail)
    {
        BookingReference = bookingReference;
        RecieverEmail = recieverEmail;
        DateSent = DateTime.Now;
    }
    public string BookingReference { get; set; }

    public string RecieverEmail { get; set; }

    public DateTime DateSent { get; set; }
}
