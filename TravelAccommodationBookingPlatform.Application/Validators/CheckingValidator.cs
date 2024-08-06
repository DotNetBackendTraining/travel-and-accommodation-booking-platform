using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Validators;

public class CheckingValidator : AbstractValidator<Checking>
{
    public CheckingValidator()
    {
        RuleFor(x => x.CheckInDate)
            .LessThanOrEqualTo(x => x.CheckOutDate)
            .WithMessage("Check-in date must be before the check-out date.");

        RuleFor(x => x.CheckOutDate)
            .GreaterThanOrEqualTo(x => x.CheckInDate)
            .WithMessage("Check-out date must be after the check-in date.");
    }
}