using System.Linq.Expressions;
using System.Reflection;
using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.Shared.Notifications;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;

public class ImageRepository : IImageRepository, ITransactionHandler
{
    private readonly IImageStorageService _imageStorageService;
    private readonly List<Func<Task>> _saveActions = [];
    private readonly List<Func<Task>> _deleteActions = [];
    private readonly List<string> _addedImageUrls = [];

    public ImageRepository(IImageStorageService imageStorageService)
    {
        _imageStorageService = imageStorageService;
    }

    public void SaveAndSet<T>(
        IFile image,
        T entity,
        Expression<Func<T, Image>> imageSelector)
    {
        _saveActions.Add(async () =>
        {
            var imageSaveResult = await _imageStorageService.SaveAllAsync([image]);
            if (imageSaveResult.IsFailure)
            {
                throw new Exception(imageSaveResult.Error);
            }

            var imageUrl = imageSaveResult.Value.First();
            _addedImageUrls.Add(imageUrl);

            var imageProperty = (PropertyInfo)((MemberExpression)imageSelector.Body).Member;
            imageProperty.SetValue(entity, new Image { Url = imageUrl });
        });
    }

    public void SaveAndSetAll<T>(
        IEnumerable<IFile> images,
        T entity,
        Expression<Func<T, ICollection<Image>>> imageCollectionSelector)
    {
        _saveActions.Add(async () =>
        {
            var imageSaveResult = await _imageStorageService.SaveAllAsync(images);
            if (imageSaveResult.IsFailure)
            {
                throw new Exception(imageSaveResult.Error);
            }

            var imageUrls = imageSaveResult.Value;
            _addedImageUrls.AddRange(imageUrls);

            var imageCollectionProperty = (PropertyInfo)((MemberExpression)imageCollectionSelector.Body).Member;
            var imageCollection = (ICollection<Image>?)imageCollectionProperty.GetValue(entity) ?? new List<Image>();
            imageCollection.Clear();

            foreach (var url in imageUrls)
            {
                imageCollection.Add(new Image { Url = url });
            }
        });
    }

    public void Delete<T>(T entity, Expression<Func<T, Image>> imageSelector)
    {
        _deleteActions.Add(async () =>
        {
            var imageProperty = (PropertyInfo)((MemberExpression)imageSelector.Body).Member;
            var image = (Image?)imageProperty.GetValue(entity);
            ArgumentNullException.ThrowIfNull(image);

            await _imageStorageService.DeleteAsync(image.Url);
            imageProperty.SetValue(entity, null);
        });
    }

    public void DeleteAll<T>(T entity, Expression<Func<T, ICollection<Image>>> imageCollectionSelector,
        Func<Image, bool>? predicate = null)
    {
        _deleteActions.Add(async () =>
        {
            var imageCollectionProperty = (PropertyInfo)((MemberExpression)imageCollectionSelector.Body).Member;
            var imageCollection = (ICollection<Image>?)imageCollectionProperty.GetValue(entity);
            ArgumentNullException.ThrowIfNull(imageCollection);

            var imagesToDelete = predicate is not null
                ? imageCollection.Where(predicate).ToList()
                : imageCollection.ToList();

            foreach (var image in imagesToDelete)
            {
                await _imageStorageService.DeleteAsync(image.Url);
            }

            foreach (var image in imagesToDelete)
            {
                imageCollection.Remove(image);
            }
        });
    }

    public async Task Handle(TransactionStartNotification notification, CancellationToken cancellationToken)
    {
        foreach (var action in _saveActions)
        {
            await action();
        }
    }

    public async Task Handle(TransactionSuccessNotification notification, CancellationToken cancellationToken)
    {
        foreach (var action in _deleteActions)
        {
            await action();
        }
    }

    public async Task Handle(TransactionFailureNotification notification, CancellationToken cancellationToken)
    {
        foreach (var url in _addedImageUrls)
        {
            await _imageStorageService.DeleteAsync(url);
        }
    }

    public Task Handle(TransactionCleanupNotification notification, CancellationToken cancellationToken)
    {
        _addedImageUrls.Clear();
        _saveActions.Clear();
        _deleteActions.Clear();
        return Task.CompletedTask;
    }
}