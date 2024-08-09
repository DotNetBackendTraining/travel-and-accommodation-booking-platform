using System.Linq.Expressions;
using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;

/// <summary>
/// Provides basic Add, Update, and Delete operations for images within a unit of work transaction.
/// </summary>
public interface IImageRepository
{
    /// <summary>
    /// Saves an image and updates the specified property of the entity.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="image">The image file to be saved.</param>
    /// <param name="entity">The entity to which the image will be set.</param>
    /// <param name="imageSelector">An expression to select the image property of the entity.</param>
    void SaveAndUpdate<T>(
        IFile image,
        T entity,
        Expression<Func<T, Image>> imageSelector);

    /// <summary>
    /// Saves multiple images and updates the specified collection property of the entity.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="images">The collection of image files to be saved.</param>
    /// <param name="entity">The entity to which the images will be set.</param>
    /// <param name="imageCollectionSelector">An expression to select the image collection property of the entity.</param>
    void SaveAndUpdateAll<T>(
        IEnumerable<IFile> images,
        T entity,
        Expression<Func<T, ICollection<Image>>> imageCollectionSelector);

    /// <summary>
    /// Saves an image and adds it to the specified collection property of the entity.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="image">The image file to be saved.</param>
    /// <param name="entity">The entity to which the image will be added.</param>
    /// <param name="imageCollectionSelector">An expression to select the image collection property of the entity.</param>
    void SaveAndAdd<T>(
        IFile image,
        T entity,
        Expression<Func<T, ICollection<Image>>> imageCollectionSelector);

    /// <summary>
    /// Deletes an image by its URL.
    /// </summary>
    /// <param name="imageUrl">The URL of the image to be deleted.</param>
    void Delete(string imageUrl);

    /// <summary>
    /// Deletes multiple images by their URLs.
    /// </summary>
    /// <param name="imageUrls">The URLs of the images to be deleted.</param>
    void DeleteAll(IEnumerable<string> imageUrls);
}