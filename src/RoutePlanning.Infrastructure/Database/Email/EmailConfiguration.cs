using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RoutePlanning.Infrastructure.Database.Email;
public sealed class EmailConfiguration : IEntityTypeConfiguration<Domain.Email.Email>
{

    public void Configure(EntityTypeBuilder<Domain.Email.Email> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BookingReference).IsRequired();
        builder.Property(x => x.RecieverEmail).IsRequired().HasMaxLength(255);
        builder.Property(x => x.DateSent).IsRequired();
    }
}
