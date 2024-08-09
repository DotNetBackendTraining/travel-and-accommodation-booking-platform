using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validators;
using TravelAccommodationBookingPlatform.Application.Validators.Extensions;

namespace TravelAccommodationBookingPlatform.Application.Cities.Commands.CreateCity;

public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
{
    public CreateCityCommandValidator()
    {
        RuleFor(x => x.Name)
            .ValidCityName();

        RuleFor(x => x.Country)
            .SetValidator(new CountryValidator());

        RuleFor(x => x.PostOffice)
            .SetValidator(new PostOfficeValidator());
    }
}