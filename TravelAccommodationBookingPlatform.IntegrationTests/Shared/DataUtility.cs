using Microsoft.AspNetCore.Http;
using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
using TravelAccommodationBookingPlatform.Presentation.Shared;

namespace TravelAccommodationBookingPlatform.IntegrationTests.Shared;

public static class DataUtility
{
    public static IFile GetTestImageFile()
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "test_image.png");
        var fileStream = File.OpenRead(filePath);
        var formFile = new FormFile(fileStream, 0, fileStream.Length, "test_image", "test_image.png");
        return new FormFileWrapper(formFile);
    }
}