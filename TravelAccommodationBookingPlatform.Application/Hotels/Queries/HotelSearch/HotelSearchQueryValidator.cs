using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Constants;
using TravelAccommodationBookingPlatform.Application.Shared.Validators;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;

public class HotelSearchQueryValidator : AbstractValidator<HotelSearchQuery>
{
    public HotelSearchQueryValidator()
    {
        RuleFor(x => x.Filters.SearchTerm)
            .NotEmpty().WithMessage("Search term must not be empty")
            .MaximumLength(ApplicationRules.Search.SearchTermMaxLength).WithMessage(
                $"Search term must be less than or equal {ApplicationRules.Search.SearchTermMaxLength} characters.");

        RuleFor(x => x.Filters.General.Checking)
            .SetValidator(new CheckingValidator());

        RuleFor(x => x.Filters.General.Checking.CheckInDate)
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("Check-in date cannot be in the past.");

        RuleFor(x => x.Filters.General.NumberOfGuests)
            .SetValidator(new NumberOfGuestsValidator());

        RuleFor(x => x.Filters.General.Rooms)
            .GreaterThan(0)
            .WithMessage("There must be at least one room.");

        RuleFor(x => x.Filters.Advanced.MinPrice)
            .SetValidator(new PriceValidator()!)
            .When(x => x.Filters.Advanced.MinPrice is not null);

        RuleFor(x => x.Filters.Advanced.MaxPrice)
            .SetValidator(new PriceValidator()!)
            .When(x => x.Filters.Advanced.MaxPrice is not null);

        RuleFor(x => x)
            .Must(x => x.Filters.Advanced.MaxPrice!.Value >= x.Filters.Advanced.MinPrice!.Value)
            .WithMessage("Max price must be greater than or equal min price.")
            .When(x => x.Filters.Advanced is { MinPrice: not null, MaxPrice: not null });

        RuleFor(x => x.PaginationParameters)
            .SetValidator(new PaginationParametersValidator());
    }
}