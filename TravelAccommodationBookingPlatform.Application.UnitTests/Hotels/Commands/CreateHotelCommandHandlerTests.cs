using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TravelAccommodationBookingPlatform.Application.Cities.Specifications;
using TravelAccommodationBookingPlatform.Application.Hotels.Commands.CreateHotel;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.IO;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Commands;

public class CreateHotelCommandHandlerTests
{
    [Theory, AutoMoqData]
    public async Task Handle_ReturnsFailure_WhenCityNotFound(
        [Frozen] Mock<IRepository<City>> mockCityRepository,
        CreateHotelCommandHandler handler,
        CreateHotelCommand command)
    {
        // Arrange
        mockCityRepository.Setup(repo => repo.ExistsAsync(
                It.IsAny<CityByIdSpecification>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.City.IdNotFound);
    }

    [Theory, AutoMoqData]
    public async Task Handle_ReturnsSuccess_WhenHotelIsCreated(
        [Frozen] Mock<IRepository<City>> mockCityRepository,
        [Frozen] Mock<ICudRepository<Hotel>> mockHotelCudRepository,
        [Frozen] Mock<IImageRepository> mockImageRepository,
        [Frozen] Mock<IImageStorageService> mockImageStorageService,
        [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
        CreateHotelCommandHandler handler,
        CreateHotelCommand command,
        List<string> imageUrls)
    {
        // Arrange
        mockCityRepository.Setup(repo => repo.ExistsAsync(
                It.IsAny<CityByIdSpecification>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        mockImageStorageService.Setup(service => service.SaveAllAsync(
                It.IsAny<IEnumerable<IFile>>()))
            .ReturnsAsync(Result.Success(imageUrls));
        mockUnitOfWork.Setup(unit => unit.SaveChangesAsync(
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockHotelCudRepository.Verify(r => r.Add(It.IsAny<Hotel>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}