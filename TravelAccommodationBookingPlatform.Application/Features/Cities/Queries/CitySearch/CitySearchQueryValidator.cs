using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validators;

namespace TravelAccommodationBookingPlatform.Application.Features.Cities.Queries.CitySearch;

public class CitySearchQueryValidator : AbstractValidator<CitySearchQuery>
{
    public CitySearchQueryValidator()
    {
        RuleFor(q => q.PaginationParameters)
            .SetValidator(new PaginationParametersValidator());
    }
}