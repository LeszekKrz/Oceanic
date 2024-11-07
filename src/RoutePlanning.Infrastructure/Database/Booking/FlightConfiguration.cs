using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace RoutePlanning.Infrastructure.Database.Booking;
public sealed class FlightConfiguration : IEntityTypeConfiguration<Domain.Booking.Flight>
{
    public void Configure(EntityTypeBuilder<Domain.Booking.Flight> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BookingReference).IsRequired();
        builder.Property(x => x.Origin).IsRequired();
        builder.Property(x => x.Destination).IsRequired();
        builder.Property(x => x.Time).IsRequired();
    }
}
