using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations.PropertyBuilderExtensions;

public static class ComplexPropertyBuilderExtensions
{
    public static ComplexPropertyBuilder<T> ApplyCoordinatesConfiguration<T>(this ComplexPropertyBuilder<T> builder)
        where T : Coordinates
    {
        builder.Property(c => c.Latitude)
            .IsRequired()
            .HasPrecision(DomainRules.Locations.PrecisionDigits, DomainRules.Locations.ScaleDigits)
            .HasAnnotation("Min", DomainRules.Locations.LatitudeMin)
            .HasAnnotation("Max", DomainRules.Locations.LatitudeMax);

        builder.Property(c => c.Longitude)
            .IsRequired()
            .HasPrecision(DomainRules.Locations.PrecisionDigits, DomainRules.Locations.ScaleDigits)
            .HasAnnotation("Min", DomainRules.Locations.LongitudeMin)
            .HasAnnotation("Max", DomainRules.Locations.LongitudeMax);

        return builder;
    }

    public static ComplexPropertyBuilder<T> ApplyPriceConfiguration<T>(this ComplexPropertyBuilder<T> builder)
        where T : Price
    {
        builder.Property(p => p.Value)
            .IsRequired()
            .HasAnnotation("Min", DomainRules.Prices.PriceMin);

        return builder;
    }

    public static ComplexPropertyBuilder<T> ApplyNumberOfGuestsConfiguration<T>(this ComplexPropertyBuilder<T> builder)
        where T : NumberOfGuests
    {
        builder.Property(g => g.Adults)
            .IsRequired()
            .HasAnnotation("Min", DomainRules.NumberOfGuests.AdultsMin)
            .HasAnnotation("Max", DomainRules.NumberOfGuests.AdultsMax);

        builder.Property(g => g.Children)
            .IsRequired()
            .HasAnnotation("Min", DomainRules.NumberOfGuests.ChildrenMin)
            .HasAnnotation("Max", DomainRules.NumberOfGuests.ChildrenMax);

        return builder;
    }

    public static ComplexPropertyBuilder<T> ApplyCountryConfiguration<T>(this ComplexPropertyBuilder<T> builder)
        where T : Country
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(DomainRules.Countries.NameMaxLength);

        return builder;
    }

    public static ComplexPropertyBuilder<T> ApplyPostOfficeConfiguration<T>(this ComplexPropertyBuilder<T> builder)
        where T : PostOffice
    {
        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(DomainRules.PostOffices.AddressMaxLength);

        builder.Property(p => p.Description)
            .HasMaxLength(DomainRules.PostOffices.DescriptionMaxLength);

        return builder;
    }
}