using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RoutePlanning.Infrastructure.Database.Price;
public sealed class PriceConfiguration : IEntityTypeConfiguration<Domain.Price.Price>
{
    public void Configure(EntityTypeBuilder<Domain.Price.Price> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ParcelType);
        builder.Property(x => x.Cost);
        
    }
}
