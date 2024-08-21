using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validators;

namespace TravelAccommodationBookingPlatform.Application.Features.Bookings.Commands.CreateBooking;

public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(x => x.RoomIds)
            .NotEmpty().WithMessage("Booking must have at least one room")
            .Must(BeUnique).WithMessage("Booking rooms must be unique");

        RuleFor(x => x.Checking)
            .SetValidator(new CheckingValidator());

        RuleFor(x => x.Checking.CheckInDate)
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("Check-in date cannot be in the past.");

        RuleFor(x => x.NumberOfGuests)
            .SetValidator(new NumberOfGuestsValidator());

        RuleFor(x => x.SpecialRequest)
            .SetValidator(new SpecialRequestValidator());
    }

    private static bool BeUnique<T>(IReadOnlyCollection<T> list)
    {
        return list.Distinct().Count() == list.Count;
    }
}