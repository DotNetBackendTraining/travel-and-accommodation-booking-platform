using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Validators;

public class SpecialRequestValidator : AbstractValidator<SpecialRequest>
{
    public SpecialRequestValidator()
    {
        RuleFor(x => x.Request)
            .MaximumLength(DomainRules.SpecialRequests.RequestMaxLength)
            .WithMessage($"Special request cannot exceed {DomainRules.SpecialRequests.RequestMaxLength} characters.");
    }
}