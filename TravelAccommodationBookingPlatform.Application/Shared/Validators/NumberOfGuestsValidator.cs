using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Shared.Validators;

public class NumberOfGuestsValidator : AbstractValidator<NumberOfGuests>
{
    public NumberOfGuestsValidator()
    {
        RuleFor(x => x.Adults)
            .InclusiveBetween(DomainRules.NumberOfGuests.AdultsMin, DomainRules.NumberOfGuests.AdultsMax)
            .WithMessage(
                $"The number of adults must be between {DomainRules.NumberOfGuests.AdultsMin} and {DomainRules.NumberOfGuests.AdultsMax}.");

        RuleFor(x => x.Children)
            .InclusiveBetween(DomainRules.NumberOfGuests.ChildrenMin, DomainRules.NumberOfGuests.ChildrenMax)
            .WithMessage(
                $"The number of children must be between {DomainRules.NumberOfGuests.ChildrenMin} and {DomainRules.NumberOfGuests.ChildrenMax}.");
    }
}