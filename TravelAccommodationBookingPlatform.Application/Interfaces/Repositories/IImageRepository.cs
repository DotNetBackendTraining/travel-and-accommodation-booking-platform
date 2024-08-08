using System.Linq.Expressions;
using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;

/// <summary>
/// Provides basic Save and Delete operations for images within a unit of work transaction.
/// </summary>
public interface IImageRepository
{
    /// <summary>
    /// Saves an image and sets it to the specified property of the entity.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="image">The image file to be saved.</param>
    /// <param name="entity">The entity to which the image will be set.</param>
    /// <param name="imageSelector">An expression to select the image property of the entity.</param>
    void SaveAndSet<T>(
        IFile image,
        T entity,
        Expression<Func<T, Image>> imageSelector);

    /// <summary>
    /// Saves multiple images and sets them to the specified collection property of the entity.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="images">The collection of image files to be saved.</param>
    /// <param name="entity">The entity to which the images will be set.</param>
    /// <param name="imageCollectionSelector">An expression to select the image collection property of the entity.</param>
    void SaveAndSetAll<T>(
        IEnumerable<IFile> images,
        T entity,
        Expression<Func<T, ICollection<Image>>> imageCollectionSelector);

    /// <summary>
    /// Deletes an image associated with the specified property of the entity.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="entity">The entity from which the image will be deleted.</param>
    /// <param name="imageSelector">An expression to select the image property of the entity.</param>
    void Delete<T>(
        T entity,
        Expression<Func<T, Image>> imageSelector);

    /// <summary>
    /// Deletes multiple images associated with the specified collection property of the entity.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="entity">The entity from which the images will be deleted.</param>
    /// <param name="imageCollectionSelector">An expression to select the image collection property of the entity.</param>
    /// <param name="predicate">An optional predicate to filter the images to be deleted.</param>
    void DeleteAll<T>(
        T entity,
        Expression<Func<T, ICollection<Image>>> imageCollectionSelector,
        Func<Image, bool>? predicate = null);
}