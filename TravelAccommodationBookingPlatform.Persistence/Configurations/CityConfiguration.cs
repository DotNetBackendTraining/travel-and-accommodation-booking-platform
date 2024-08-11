using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Persistence.Configurations.PropertyBuilderExtensions;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(DomainRules.Cities.NameMaxLength);

        builder.ComplexProperty(c => c.Country)
            .ApplyCountryConfiguration();

        builder.ComplexProperty(c => c.PostOffice)
            .ApplyPostOfficeConfiguration();

        builder.HasOne(r => r.ThumbnailImage)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Hotels)
            .WithOne(h => h.City)
            .HasForeignKey(h => h.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}