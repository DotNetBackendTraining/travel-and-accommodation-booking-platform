using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validators;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelReviews;

public class HotelReviewsQueryValidator : AbstractValidator<HotelReviewsQuery>
{
    public HotelReviewsQueryValidator()
    {
        RuleFor(q => q.PaginationParameters)
            .SetValidator(new PaginationParametersValidator());
    }
}