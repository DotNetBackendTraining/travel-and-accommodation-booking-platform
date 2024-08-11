using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Persistence.Configurations.Associations;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations;

public class HotelImageAssociationConfiguration : IEntityTypeConfiguration<HotelImageAssociation>
{
    public void Configure(EntityTypeBuilder<HotelImageAssociation> builder)
    {
        builder.HasOne(ia => ia.Image)
            .WithMany()
            .HasForeignKey(ia => ia.ImageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ia => ia.ImageId);
        builder.HasIndex(ia => ia.HotelId);
    }
}