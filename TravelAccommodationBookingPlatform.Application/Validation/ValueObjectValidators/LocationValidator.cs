using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Validation.ValueObjectValidators;

public class LocationValidator : AbstractValidator<Location>
{
    public LocationValidator()
    {
        RuleFor(x => x.City)
            .NotNull().WithMessage("City is required.");

        RuleFor(x => x.Coordinates)
            .SetValidator(new CoordinatesValidator());
    }
}