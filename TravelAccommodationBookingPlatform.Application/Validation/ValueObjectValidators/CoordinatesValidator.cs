using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Validation.ValueObjectValidators;

public class CoordinatesValidator : AbstractValidator<Coordinates>
{
    public CoordinatesValidator()
    {
        RuleFor(x => x.Latitude)
            .InclusiveBetween(DomainRules.Locations.LatitudeMin, DomainRules.Locations.LatitudeMax)
            .WithMessage(
                $"Latitude must be between {DomainRules.Locations.LatitudeMin} and {DomainRules.Locations.LatitudeMax}.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(DomainRules.Locations.LongitudeMin, DomainRules.Locations.LongitudeMax)
            .WithMessage(
                $"Longitude must be between {DomainRules.Locations.LongitudeMin} and {DomainRules.Locations.LongitudeMax}.");
    }
}