using Ardalis.Specification;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TravelAccommodationBookingPlatform.Application.Hotels.Commands.DeleteHotel;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Commands;

public class DeleteHotelCommandHandlerTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_HotelNotFound_ReturnsFailure(
        [Frozen] Mock<IRepository<Hotel>> hotelRepositoryMock,
        DeleteHotelCommand request,
        DeleteHotelCommandHandler handler)
    {
        // Arrange
        hotelRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Hotel>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Hotel?)null);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Hotel.IdNotFound);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_HotelHasBookings_ReturnsFailure(
        [Frozen] Mock<IRepository<Hotel>> hotelRepositoryMock,
        [Frozen] Mock<IRepository<Hotel>> hotelWithoutBookingsRepositoryMock,
        Hotel hotel,
        DeleteHotelCommand request,
        DeleteHotelCommandHandler handler)
    {
        // Arrange
        hotelRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Hotel>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(hotel);

        hotelWithoutBookingsRepositoryMock.Setup(repo => repo.ExistsAsync(
                It.IsAny<Specification<Hotel>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Hotel.CannotDeleteHotelWithBookings);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_SuccessfullyDeletesHotel_ReturnsSuccess(
        [Frozen] Mock<IRepository<Hotel>> hotelRepositoryMock,
        [Frozen] Mock<ICudRepository<Hotel>> cudHotelRepositoryMock,
        [Frozen] Mock<IImageRepository> imageRepositoryMock,
        [Frozen] Mock<IUnitOfWork> unitOfWorkMock,
        Hotel hotel,
        DeleteHotelCommand request,
        DeleteHotelCommandHandler handler)
    {
        // Arrange
        hotelRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Hotel>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(hotel);

        hotelRepositoryMock.Setup(repo => repo.ExistsAsync(
                It.IsAny<Specification<Hotel>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        imageRepositoryMock.Setup(repo => repo.Delete(It.IsAny<string>()));
        imageRepositoryMock.Setup(repo => repo.DeleteAll(It.IsAny<List<string>>()));
        cudHotelRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Hotel>()));
        unitOfWorkMock.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        cudHotelRepositoryMock.Verify(repo => repo.Delete(hotel), Times.Once);
        imageRepositoryMock.Verify(repo => repo.Delete(hotel.ThumbnailImage.Url), Times.Once);
        imageRepositoryMock.Verify(repo => repo.DeleteAll(It.IsAny<List<string>>()), Times.Once);
        unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}