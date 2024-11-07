using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RoutePlanning.Infrastructure.Database.Parcel;
public sealed class ParcelConfiguration : IEntityTypeConfiguration<Domain.Parcel.Parcel>
{
    public void Configure(EntityTypeBuilder<Domain.Parcel.Parcel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BookingReference);
        builder.Property(x => x.Height);
        builder.Property(x => x.Weight);
        builder.Property(x => x.Width);
    }
}
