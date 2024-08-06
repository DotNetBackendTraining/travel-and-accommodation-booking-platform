using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Validators;

public class PostOfficeValidator : AbstractValidator<PostOffice>
{
    public PostOfficeValidator()
    {
        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Address is required.")
            .MaximumLength(DomainRules.PostOffices.AddressMaxLength)
            .WithMessage($"Address cannot exceed {DomainRules.PostOffices.AddressMaxLength} characters.");

        RuleFor(x => x.Description)
            .MaximumLength(DomainRules.PostOffices.DescriptionMaxLength)
            .WithMessage($"Description cannot exceed {DomainRules.PostOffices.DescriptionMaxLength} characters.");
    }
}