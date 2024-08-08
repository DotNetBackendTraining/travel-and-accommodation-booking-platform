using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.IntegrationTests.Shared;

namespace TravelAccommodationBookingPlatform.IntegrationTests;

[Collection("IntegrationTests")]
public class ImageTransactionIntegrationTests : BaseIntegrationTest
{
    private readonly IImageRepository _imageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ImageTransactionIntegrationTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        _imageRepository = Scope.ServiceProvider.GetRequiredService<IImageRepository>();
        _unitOfWork = Scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    }

    [Fact]
    public async Task SaveAndDeleteImageTransactionTest()
    {
        // Arrange
        var entity = new DummyEntity();
        var imageFile = DataUtility.GetTestImageFile();

        // Act
        _imageRepository.SaveAndSet(imageFile, entity, e => e.Image);

        // Assert
        entity.Image.Should().BeNull();

        // Act
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);

        // Assert
        entity.Image.Should().NotBeNull();
        Uri.IsWellFormedUriString(entity.Image.Url, UriKind.Absolute).Should().BeTrue();

        // Act
        _imageRepository.Delete(entity, e => e.Image);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);

        // Assert
        entity.Image.Should().BeNull();
    }

    private class DummyEntity
    {
        public Image Image { get; set; } = default!;
    }
}