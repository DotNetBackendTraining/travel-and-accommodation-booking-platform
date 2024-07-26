namespace TravelAccommodationBookingPlatform.Application.Interfaces.Files;

public interface IImageStorageService
{
    /// <returns>List of image URLs</returns>
    /// <exception cref="Exception">If an error occurs</exception>
    Task<List<string>> SaveAllAsync(IEnumerable<IFile> images);

    /// <exception cref="Exception">If an error occurs</exception>
    Task DeleteAsync(string imageUrl);
}