using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoutePlanning.Domain.Booking;
using RoutePlanning.Domain.Users;

namespace RoutePlanning.Infrastructure.Database.Booking;
public sealed class BookingConfiguration : IEntityTypeConfiguration<Domain.Booking.Booking>
{
    public void Configure(EntityTypeBuilder<Domain.Booking.Booking> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Reference).IsRequired();
        builder.Property(x => x.Origin).IsRequired();
        builder.Property(x => x.Destination).IsRequired();
        builder.Property(x => x.Price);
        builder.Property(x => x.Revenue).IsRequired();
        builder.Property(x => x.Time).IsRequired();
    }
}
