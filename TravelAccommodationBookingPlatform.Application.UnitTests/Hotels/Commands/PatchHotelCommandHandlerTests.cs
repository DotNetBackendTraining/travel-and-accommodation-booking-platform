using Ardalis.Specification;
using AutoFixture.Xunit2;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Commands.PatchHotel;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Commands;

public class PatchHotelCommandHandlerTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_HotelNotFound_ReturnsFailure(
        [Frozen] Mock<IRepository<Hotel>> hotelRepositoryMock,
        PatchHotelCommand request,
        PatchHotelCommandHandler handler)
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
    public async Task Handle_ValidationFails_ReturnsFailure(
        [Frozen] Mock<IRepository<Hotel>> hotelRepositoryMock,
        [Frozen] Mock<IValidator<PatchHotelModel>> validatorMock,
        Hotel hotel,
        PatchHotelCommand request,
        PatchHotelCommandHandler handler)
    {
        // Arrange
        hotelRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Hotel>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(hotel);

        validatorMock.Setup(v => v.ValidateAsync(
                It.IsAny<ValidationContext<PatchHotelModel>>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ValidationException([new ValidationFailure("Property", "Error")]));

        // Act
        Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_CityNotFound_ReturnsFailure(
        [Frozen] Mock<IRepository<Hotel>> hotelRepositoryMock,
        [Frozen] Mock<IRepository<City>> cityRepositoryMock,
        Hotel hotel,
        PatchHotelCommand request,
        PatchHotelCommandHandler handler)
    {
        // Arrange
        hotelRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Hotel>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(hotel);

        cityRepositoryMock.Setup(repo => repo.ExistsAsync(
                It.IsAny<Specification<City>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.City.IdNotFound);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_SuccessfullyUpdatesHotel_ReturnsSuccess(
        [Frozen] Mock<IRepository<Hotel>> hotelRepositoryMock,
        [Frozen] Mock<IRepository<City>> cityRepositoryMock,
        [Frozen] Mock<ICudRepository<Hotel>> cudHotelRepositoryMock,
        [Frozen] Mock<IUnitOfWork> unitOfWorkMock,
        Hotel hotel,
        PatchHotelCommand request,
        PatchHotelCommandHandler handler)
    {
        // Arrange
        hotelRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Hotel>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(hotel);

        cityRepositoryMock.Setup(repo => repo.ExistsAsync(
                It.IsAny<Specification<City>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        cudHotelRepositoryMock.Setup(repo => repo.Update(It.IsAny<Hotel>()));
        unitOfWorkMock.Setup(uow => uow.CommitAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        cudHotelRepositoryMock.Verify(repo => repo.Update(hotel), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}