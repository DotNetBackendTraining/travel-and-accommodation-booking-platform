using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Persistence.Configurations.PropertyBuilderExtensions;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.Property(h => h.Name)
            .IsRequired()
            .HasMaxLength(DomainRules.Hotels.NameMaxLength);

        builder.Property(h => h.Description)
            .IsRequired()
            .HasMaxLength(DomainRules.Hotels.DescriptionMaxLength);

        builder.Property(h => h.Owner)
            .IsRequired()
            .HasMaxLength(DomainRules.Hotels.OwnerMaxLength);

        builder.Property(h => h.StarRate)
            .IsRequired()
            .HasAnnotation("Min", DomainRules.Hotels.StarRateMin)
            .HasAnnotation("Max", DomainRules.Hotels.StarRateMax);

        builder.OwnsOne(h => h.ThumbnailImage, img =>
            img.ApplyImageConfiguration());

        builder.HasOne(h => h.City)
            .WithMany()
            .HasForeignKey(h => h.CityId);

        builder.ComplexProperty(h => h.Coordinates)
            .ApplyCoordinatesConfiguration();

        builder.HasMany(h => h.Rooms)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId);

        builder.OwnsMany(h => h.Images, img =>
            img.ApplyImageConfiguration());

        builder.OwnsMany(h => h.Reviews, rev =>
            rev.ApplyReviewConfiguration());

        builder.HasMany(h => h.Discounts)
            .WithOne();
    }
}