using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validators;
using TravelAccommodationBookingPlatform.Application.Validators.Extensions;

namespace TravelAccommodationBookingPlatform.Application.Features.Cities.Commands.PatchCity;

public class PatchCityModelValidator : AbstractValidator<PatchCityModel>
{
    public PatchCityModelValidator()
    {
        RuleFor(x => x.Name!)
            .NotNull().WithMessage("Name field cannot be removed")
            .ValidCityName();

        RuleFor(x => x.Country!)
            .NotNull().WithMessage("Country field cannot be removed")
            .SetValidator(new CountryValidator());

        RuleFor(x => x.PostOffice!)
            .NotNull().WithMessage("PostOffice field cannot be removed")
            .SetValidator(new PostOfficeValidator());
    }
}