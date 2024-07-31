using TravelAccommodationBookingPlatform.Application.Constants;

namespace TravelAccommodationBookingPlatform.Presentation.Attributes;

public class ValidImageExtensionsAttribute : ValidFileExtensionsAttribute
{
    public ValidImageExtensionsAttribute() : base(GetAllowedExtensions())
    {
    }

    private static string[] GetAllowedExtensions()
    {
        return Enum.GetNames(typeof(ApplicationRules.File.ImageExtensions))
            .Select(x => x.ToLower())
            .ToArray();
    }
}