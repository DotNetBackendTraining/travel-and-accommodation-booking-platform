using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
using TravelAccommodationBookingPlatform.Infrastructure.Settings;

namespace TravelAccommodationBookingPlatform.Infrastructure.Services.Files;

public class CloudinaryImageStorageService : IImageStorageService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryImageStorageService(IOptions<CloudinarySettings> options)
    {
        var settings = options.Value ??
                       throw new ArgumentException(nameof(CloudinarySettings) + " not found");

        _cloudinary = new Cloudinary(new Account(
            settings.CloudName,
            settings.ApiKey,
            settings.ApiSecret));
    }

    public async Task<List<string>> SaveAllAsync(IEnumerable<IFile> images)
    {
        var imageUrls = new List<string>();

        foreach (var file in images)
        {
            using var memoryStream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, memoryStream),
            };

            var result = _cloudinary.Upload(uploadParams);
            if (result.Error != null)
            {
                throw new Exception($"Cloudinary error occurred: {result.Error.Message}");
            }

            imageUrls.Add(result.SecureUrl.ToString());
        }

        return imageUrls;
    }
}