using Ardalis.Specification;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TravelAccommodationBookingPlatform.Application.Cities.Commands.DeleteCity;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Cities.Commands;

public class DeleteCityCommandHandlerTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_CityNotFound_ReturnsFailure(
        [Frozen] Mock<IRepository<City>> cityRepositoryMock,
        DeleteCityCommand request,
        DeleteCityCommandHandler handler)
    {
        // Arrange
        cityRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<City>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((City?)null);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.City.IdNotFound);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_CityHasHotels_ReturnsFailure(
        [Frozen] Mock<IRepository<City>> cityRepositoryMock,
        City city,
        DeleteCityCommand request,
        DeleteCityCommandHandler handler)
    {
        // Arrange
        cityRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<City>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(city);

        cityRepositoryMock.Setup(repo => repo.ExistsAsync(
                It.IsAny<Specification<City>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false); // Meaning city has hotels

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.City.CannotDeleteCityWithHotels);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_SuccessfullyDeletesCity_ReturnsSuccess(
        [Frozen] Mock<IRepository<City>> cityRepositoryMock,
        [Frozen] Mock<ICudRepository<City>> cudCityRepositoryMock,
        [Frozen] Mock<IImageRepository> imageRepositoryMock,
        [Frozen] Mock<IUnitOfWork> unitOfWorkMock,
        City city,
        DeleteCityCommand request,
        DeleteCityCommandHandler handler)
    {
        // Arrange
        cityRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<City>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(city);

        cityRepositoryMock.Setup(repo => repo.ExistsAsync(
                It.IsAny<Specification<City>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true); // Meaning city has not hotels

        imageRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Image>()));
        cudCityRepositoryMock.Setup(repo => repo.Delete(It.IsAny<City>()));
        unitOfWorkMock.Setup(uow => uow.CommitAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        cudCityRepositoryMock.Verify(repo => repo.Delete(city), Times.Once);
        imageRepositoryMock.Verify(repo => repo.Delete(city.ThumbnailImage), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}