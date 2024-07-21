namespace TravelAccommodationBookingPlatform.Application.Interfaces.Files;

public interface IImageStorageService
{
    /// <returns>List of image URLs</returns>
    Task<List<string>> SaveAllAsync(IEnumerable<IFile> images);
}