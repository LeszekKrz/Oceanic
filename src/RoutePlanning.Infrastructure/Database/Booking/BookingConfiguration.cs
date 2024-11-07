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
        builder.Property(x => x.BookedByUserId).IsRequired();

        builder.HasOne(b => b.Email)
                .WithOne()
                .HasForeignKey<Domain.Email.Email>(e => e.BookingReference);

        builder.HasOne(b => b.Parcel)
            .WithOne()
            .HasForeignKey<Domain.Parcel.Parcel>(p => p.BookingReference);

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Domain.Booking.Booking>(b => b.BookedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
