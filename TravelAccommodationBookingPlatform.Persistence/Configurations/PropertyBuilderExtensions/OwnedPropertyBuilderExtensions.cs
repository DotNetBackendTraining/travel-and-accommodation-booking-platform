using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations.PropertyBuilderExtensions;

/// <summary>
/// <see cref="Image"/> is assumed to be an (owned) entity,
/// because some Entities (e.g. <see cref="Hotel"/>) own a collection of it.
/// Same thing applies for <see cref="Review"/>. EF Core doesn't support complex type collections yet.
/// </summary>
public static class OwnedPropertyBuilderExtensions
{
    public static OwnedNavigationBuilder<T, Image> ApplyImageConfiguration<T>(
        this OwnedNavigationBuilder<T, Image> builder)
        where T : class
    {
        builder.Property(i => i.Url)
            .IsRequired()
            .HasMaxLength(DomainRules.Images.UrlMaxLength);

        return builder;
    }

    public static OwnedNavigationBuilder<T, Review> ApplyReviewConfiguration<T>(
        this OwnedNavigationBuilder<T, Review> builder)
        where T : class
    {
        builder.Property(r => r.Text)
            .IsRequired()
            .HasMaxLength(DomainRules.Reviews.TextMaxLength);

        return builder;
    }
}