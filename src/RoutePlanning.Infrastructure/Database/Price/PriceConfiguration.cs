using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RoutePlanning.Infrastructure.Database.Price;
internal class PriceConfiguration : IEntityTypeConfiguration<Domain.Price.Price>
{
    public void Configure(EntityTypeBuilder<Domain.Price.Price> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Cost).IsRequired();
        builder.Property(x => x.ParcelType).IsRequired();
    }
}
