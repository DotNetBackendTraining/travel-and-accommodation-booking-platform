using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using TravelAccommodationBookingPlatform.Application.Constants;
using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
using TravelAccommodationBookingPlatform.Domain.Shared;
using TravelAccommodationBookingPlatform.Infrastructure.Settings;

namespace TravelAccommodationBookingPlatform.Infrastructure.Files;

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

    public async Task<Result<List<string>>> SaveAllAsync(IEnumerable<IFile> images)
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
            if (result.Error is not null)
            {
                return Result.Failure<List<string>>(ApplicationErrors.File.UploadFailed);
            }

            imageUrls.Add(result.SecureUrl.ToString());
        }

        return Result.Success(imageUrls);
    }

    public async Task<Result> DeleteAsync(string imageUrl)
    {
        var publicId = GetPublicIdFromUrl(imageUrl);

        var deletionParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deletionParams);

        return result.Error is not null
            ? Result.Failure(ApplicationErrors.File.DeletionFailed)
            : Result.Success();
    }

    private static string GetPublicIdFromUrl(string url)
    {
        var uri = new Uri(url);
        return Path.GetFileNameWithoutExtension(uri.LocalPath);
    }
}