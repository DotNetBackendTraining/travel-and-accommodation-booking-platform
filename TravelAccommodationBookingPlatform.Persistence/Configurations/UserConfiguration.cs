using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(DomainRules.Users.UsernameMaxLength);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(256); // typical hash size

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(DomainRules.Users.EmailMaxLength);

        builder.Property(u => u.UserRole)
            .IsRequired();

        builder.HasIndex(u => u.Username).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
    }
}