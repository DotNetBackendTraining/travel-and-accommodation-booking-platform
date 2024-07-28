using TravelAccommodationBookingPlatform.Application.Constants;

namespace TravelAccommodationBookingPlatform.Presentation.Attributes;

public class ValidImageExtensionsAttribute : ValidFileExtensionsAttribute
{
    public ValidImageExtensionsAttribute() : base(ApplicationRules.File.ImageExtensions)
    {
    }
}