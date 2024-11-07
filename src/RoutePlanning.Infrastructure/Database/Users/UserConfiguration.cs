using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoutePlanning.Domain.Users;

namespace RoutePlanning.Infrastructure.Database.Users;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Username);
        builder.Property(x => x.PasswordHash);
        builder.Property(x => x.Email);
        builder.Property(x => x.Address);
        builder.Property(x => x.PhoneNumber);
        builder.Property(x => x.IsEmployee);
        builder.Property(x => x.IsAdmin);
    }
}
