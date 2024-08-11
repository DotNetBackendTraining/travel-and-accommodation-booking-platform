using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Persistence.Configurations.Associations;
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
            .IsRequired();

        builder.HasOne(h => h.ThumbnailImage)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(h => h.City)
            .WithMany(c => c.Hotels)
            .HasForeignKey(h => h.CityId)
            .IsRequired();

        builder.ComplexProperty(h => h.Coordinates)
            .ApplyCoordinatesConfiguration();

        builder.HasMany(h => h.Rooms)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(h => h.Images)
            .WithMany()
            .UsingEntity<HotelImageAssociation>(
                j => j
                    .HasOne(ia => ia.Image)
                    .WithMany()
                    .HasForeignKey(ia => ia.ImageId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Hotel>()
                    .WithMany()
                    .HasForeignKey(ia => ia.HotelId)
                    .OnDelete(DeleteBehavior.Restrict));

        builder.OwnsMany(h => h.Reviews, rev =>
        {
            rev.WithOwner().HasForeignKey("HotelId");
            rev.Property<int>("Id");
            rev.HasKey("Id");
            rev.ApplyReviewConfiguration();
        }).Navigation(e => e.Reviews).AutoInclude(false);

        builder.HasMany(h => h.Discounts)
            .WithOne(d => d.Hotel)
            .HasForeignKey(d => d.HotelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(h => h.ActiveDiscount)
            .WithOne()
            .HasForeignKey<Hotel>(h => h.ActiveDiscountId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
    }
}