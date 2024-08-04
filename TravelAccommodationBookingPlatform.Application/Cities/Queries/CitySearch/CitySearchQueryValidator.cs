using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Shared.Validators;

namespace TravelAccommodationBookingPlatform.Application.Cities.Queries.CitySearch;

public class CitySearchQueryValidator : AbstractValidator<CitySearchQuery>
{
    public CitySearchQueryValidator()
    {
        RuleFor(q => q.PaginationParameters)
            .SetValidator(new PaginationParametersValidator());
    }
}