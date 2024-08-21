using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validators;
using TravelAccommodationBookingPlatform.Application.Validators.Extensions;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Commands.PatchHotel;

public class PatchHotelModelValidator : AbstractValidator<PatchHotelModel>
{
    public PatchHotelModelValidator()
    {
        RuleFor(x => x.Name!)
            .NotNull().WithMessage("Name field cannot be removed")
            .ValidHotelName();

        RuleFor(x => x.StarRate)
            .NotNull().WithMessage("StarRate field cannot be removed");

        RuleFor(x => x.Description!)
            .NotNull().WithMessage("Description field cannot be removed")
            .ValidHotelDescription();

        RuleFor(x => x.Owner!)
            .NotNull().WithMessage("Owner field cannot be removed")
            .ValidOwner();

        RuleFor(x => x.Coordinates!)
            .NotNull().WithMessage("Coordinates field cannot be removed")
            .SetValidator(new CoordinatesValidator());

        RuleFor(x => x.Amenities)
            .NotNull().WithMessage("Amenities field cannot be removed");

        RuleFor(x => x.CityId)
            .NotNull().WithMessage("CityId field cannot be removed");
    }
}