using System.Linq.Expressions;
using System.Reflection;
using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.Shared.Notifications;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;

public class ImageRepository : IImageRepository, ITransactionHandler
{
    private readonly IImageStorageService _imageStorageService;
    private readonly ICudRepository<Image> _imageCudRepository;
    private readonly List<Func<Task>> _saveActions = [];
    private readonly List<string> _imageUrlsToDelete = [];
    private readonly List<string> _addedImageUrls = [];

    public ImageRepository(
        IImageStorageService imageStorageService,
        ICudRepository<Image> imageCudRepository)
    {
        _imageStorageService = imageStorageService;
        _imageCudRepository = imageCudRepository;
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
            var imageEntity = (Image?)imageProperty.GetValue(entity) ?? new Image();

            imageEntity.Url = imageUrl;
            _imageCudRepository.Add(imageEntity);
            imageProperty.SetValue(entity, imageEntity);
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
            var imageCollection = (ICollection<Image>?)imageCollectionProperty.GetValue(entity) ?? [];

            imageCollection.Clear();
            foreach (var image in imageUrls.Select(url => new Image { Url = url }))
            {
                _imageCudRepository.Add(image);
                imageCollection.Add(image);
            }

            imageCollectionProperty.SetValue(entity, imageCollection);
        });
    }

    public void SaveAndAdd<T>(IFile image, T entity, Expression<Func<T, ICollection<Image>>> imageCollectionSelector)
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

            var imageCollectionProperty = (PropertyInfo)((MemberExpression)imageCollectionSelector.Body).Member;
            var imageCollection = (ICollection<Image>?)imageCollectionProperty.GetValue(entity) ?? [];

            var imageEntity = new Image { Url = imageUrl };
            _imageCudRepository.Add(imageEntity);
            imageCollection.Add(imageEntity);
            imageCollectionProperty.SetValue(entity, imageCollection);
        });
    }

    public void Delete(Image image)
    {
        _imageUrlsToDelete.Add(image.Url);
        _imageCudRepository.Delete(image);
    }

    public void DeleteAll(IEnumerable<Image> images)
    {
        foreach (var image in images)
        {
            Delete(image);
        }
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
        foreach (var url in _imageUrlsToDelete)
        {
            await _imageStorageService.DeleteAsync(url);
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
        _saveActions.Clear();
        _imageUrlsToDelete.Clear();
        _addedImageUrls.Clear();
        return Task.CompletedTask;
    }
}