using Ardalis.Specification;
using AutoFixture.Xunit2;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using TravelAccommodationBookingPlatform.Application.Features.Cities.Commands.PatchCity;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Cities.Commands;

public class PatchCityCommandHandlerTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_CityNotFound_ReturnsFailure(
        [Frozen] Mock<IRepository<City>> cityRepositoryMock,
        PatchCityCommand request,
        PatchCityCommandHandler handler)
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
    public async Task Handle_ValidationFails_ThrowsValidationException(
        [Frozen] Mock<IRepository<City>> cityRepositoryMock,
        [Frozen] Mock<IValidator<PatchCityModel>> validatorMock,
        City city,
        PatchCityCommand request,
        PatchCityCommandHandler handler)
    {
        // Arrange
        cityRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<City>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(city);

        validatorMock.Setup(v => v.ValidateAsync(
                It.IsAny<ValidationContext<PatchCityModel>>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ValidationException(new[] { new ValidationFailure("Property", "Error") }));

        // Act
        Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_SuccessfullyUpdatesCity_ReturnsSuccess(
        [Frozen] Mock<IRepository<City>> cityRepositoryMock,
        [Frozen] Mock<ICudRepository<City>> cudCityRepositoryMock,
        [Frozen] Mock<IUnitOfWork> unitOfWorkMock,
        City city,
        PatchCityCommand request,
        PatchCityCommandHandler handler)
    {
        // Arrange
        cityRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<City>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(city);

        cudCityRepositoryMock.Setup(repo => repo.Update(It.IsAny<City>()));
        unitOfWorkMock.Setup(uow => uow.CommitAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        cudCityRepositoryMock.Verify(repo => repo.Update(city), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}