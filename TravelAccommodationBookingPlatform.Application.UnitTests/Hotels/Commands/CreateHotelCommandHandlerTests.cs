using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TravelAccommodationBookingPlatform.Application.Cities.Specifications;
using TravelAccommodationBookingPlatform.Application.Hotels.Commands.CreateHotel;
using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
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
        mockCityRepository.Setup(repo => repo.ExistsAsync(
                It.IsAny<CityByIdSpecification>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.City.IdNotFound);
    }

    [Theory, AutoMoqData]
    public async Task Handle_ReturnsFailure_WhenImageSaveFails(
        [Frozen] Mock<IRepository<City>> mockCityRepository,
        [Frozen] Mock<IImageStorageService> mockImageStorageService,
        CreateHotelCommandHandler handler,
        CreateHotelCommand command,
        Error imageError)
    {
        mockCityRepository.Setup(repo => repo.ExistsAsync(
                It.IsAny<CityByIdSpecification>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        mockImageStorageService.Setup(service => service.SaveAllAsync(
                It.IsAny<IEnumerable<IFile>>()))
            .ReturnsAsync(Result.Failure<List<string>>(imageError));

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(imageError);
    }

    [Theory, AutoMoqData]
    public async Task Handle_ReturnsSuccess_WhenHotelIsCreated(
        [Frozen] Mock<IRepository<City>> mockCityRepository,
        [Frozen] Mock<ICudRepository<Hotel>> mockHotelCudRepository,
        [Frozen] Mock<IImageStorageService> mockImageStorageService,
        [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
        CreateHotelCommandHandler handler,
        CreateHotelCommand command,
        List<string> imageUrls)
    {
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

        var result = await handler.Handle(command, CancellationToken.None);

        mockHotelCudRepository.Verify(r => r.Add(It.IsAny<Hotel>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Theory, AutoMoqData]
    public async Task Handle_RollsBack_WhenUnitOfWorkThrowsException(
        [Frozen] Mock<IRepository<City>> mockCityRepository,
        [Frozen] Mock<IImageStorageService> mockImageStorageService,
        [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
        CreateHotelCommandHandler handler,
        CreateHotelCommand command,
        List<string> imageUrls)
    {
        mockCityRepository.Setup(repo =>
                repo.ExistsAsync(It.IsAny<CityByIdSpecification>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        mockImageStorageService.Setup(service => service.SaveAllAsync(It.IsAny<IEnumerable<IFile>>()))
            .ReturnsAsync(Result.Success(imageUrls));
        mockUnitOfWork.Setup(unit => unit.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Throws(new Exception("Database error"));
        mockImageStorageService.Setup(service => service.DeleteAsync(It.IsAny<string>()))
            .ReturnsAsync(Result.Success());

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>().WithMessage("Database error");
        mockImageStorageService.Verify(service => service.DeleteAsync(It.IsAny<string>()),
            Times.Exactly(imageUrls.Count));
    }
}