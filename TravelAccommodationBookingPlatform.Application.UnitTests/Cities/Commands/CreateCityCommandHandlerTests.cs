using System.Linq.Expressions;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Moq;
using TravelAccommodationBookingPlatform.Application.Features.Cities.Commands.CreateCity;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Cities.Commands;

public class CreateCityCommandHandlerTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_SuccessfulCityCreation_ReturnsSuccess(
        [Frozen] Mock<ICudRepository<City>> cudRepositoryMock,
        [Frozen] Mock<IImageRepository> imageRepositoryMock,
        [Frozen] Mock<IUnitOfWork> unitOfWorkMock,
        [Frozen] Mock<IMapper> mapperMock,
        CreateCityCommand request,
        City city,
        CreateCityCommandHandler handler)
    {
        // Arrange
        mapperMock.Setup(m => m.Map<City>(request)).Returns(city);

        cudRepositoryMock.Setup(repo => repo.Add(It.IsAny<City>()));
        imageRepositoryMock.Setup(repo => repo
            .SaveAndSet(request.ThumbnailImage, city, It.IsAny<Expression<Func<City, Image>>>()));
        unitOfWorkMock.Setup(uow => uow.CommitAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(city.Id);

        cudRepositoryMock.Verify(repo => repo.Add(city),
            Times.Once);
        imageRepositoryMock.Verify(repo => repo
                .SaveAndSet(request.ThumbnailImage, city, It.IsAny<Expression<Func<City, Image>>>()),
            Times.Once);
        unitOfWorkMock.Verify(uow => uow.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }
}