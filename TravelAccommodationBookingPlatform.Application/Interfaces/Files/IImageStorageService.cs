using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Files;

public interface IImageStorageService
{
    /// <returns>List of image URLs</returns>
    Task<Result<List<string>>> SaveAllAsync(IEnumerable<IFile> images);

    Task<Result> DeleteAsync(string imageUrl);
}