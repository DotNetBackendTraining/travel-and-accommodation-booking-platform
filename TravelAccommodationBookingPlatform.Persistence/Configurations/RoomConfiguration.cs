using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Persistence.Configurations.Associations;
using TravelAccommodationBookingPlatform.Persistence.Configurations.PropertyBuilderExtensions;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasOne(r => r.Hotel)
            .WithMany(h => h.Rooms)
            .HasForeignKey(r => r.HotelId);

        builder.Property(r => r.RoomNumber)
            .IsRequired()
            .HasAnnotation("Min", DomainRules.Rooms.RoomNumberMin)
            .HasAnnotation("Max", DomainRules.Rooms.RoomNumberMax);

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(DomainRules.Rooms.DescriptionMaxLength);

        builder.ComplexProperty(r => r.Price)
            .ApplyPriceConfiguration();

        builder.ComplexProperty(r => r.MaxNumberOfGuests)
            .ApplyNumberOfGuestsConfiguration();

        builder.HasMany(r => r.Images)
            .WithMany()
            .UsingEntity<RoomImageAssociation>(
                j => j
                    .HasOne(ia => ia.Image)
                    .WithMany()
                    .HasForeignKey(ia => ia.ImageId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Room>()
                    .WithMany()
                    .HasForeignKey(ia => ia.RoomId)
                    .OnDelete(DeleteBehavior.Restrict));

        builder.HasMany(r => r.Bookings);
    }
}