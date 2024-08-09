using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.IntegrationTests.Shared;
using TravelAccommodationBookingPlatform.Persistence;

namespace TravelAccommodationBookingPlatform.IntegrationTests;

[Collection("IntegrationTests")]
public class ImageTransactionIntegrationTests : BaseIntegrationTest
{
    private readonly AppDbContext _dbContext;
    private readonly IImageRepository _imageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ImageTransactionIntegrationTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        _dbContext = Scope.ServiceProvider.GetRequiredService<AppDbContext>();
        _imageRepository = Scope.ServiceProvider.GetRequiredService<IImageRepository>();
        _unitOfWork = Scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    }

    [Fact]
    public async Task SaveAndDeleteHotelImageTransactionTest()
    {
        // Arrange
        var hotel = _dbContext.Hotels.First();
        hotel.Images = [];
        var imageFile = DataUtility.GetTestImageFile();

        // Act
        _imageRepository.SaveAndAdd(imageFile, hotel, e => e.Images);

        // Assert
        hotel.Images.Should().BeEmpty();

        // Act
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);

        // Assert
        hotel.Images.Should().ContainSingle();
        Uri.IsWellFormedUriString(hotel.Images.First().Url, UriKind.Absolute).Should().BeTrue();

        // Act
        var image = hotel.Images.First();
        hotel.Images.Remove(image);
        _imageRepository.Delete(image.Url);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);

        // Assert
        hotel.Images.Should().BeEmpty();
    }
}