using System.Linq.Expressions;
using Ardalis.Specification;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Moq;
using TravelAccommodationBookingPlatform.Application.Features.Rooms.Commands.CreateRoom;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.IO;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Rooms.Commands;

public class CreateRoomCommandHandlerTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_HotelNotFound_ReturnsFailure(
        [Frozen] Mock<IRepository<Hotel>> hotelRepositoryMock,
        CreateRoomCommand request,
        CreateRoomCommandHandler handler)
    {
        // Arrange
        hotelRepositoryMock.Setup(repo => repo.ExistsAsync(
                It.IsAny<Specification<Hotel>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Hotel.IdNotFound);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_RoomNumberAlreadyExists_ReturnsFailure(
        [Frozen] Mock<IRepository<Hotel>> hotelRepositoryMock,
        [Frozen] Mock<IRepository<Room>> roomRepositoryMock,
        CreateRoomCommand request,
        CreateRoomCommandHandler handler)
    {
        // Arrange
        hotelRepositoryMock.Setup(repo => repo.ExistsAsync(
                It.IsAny<Specification<Hotel>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        roomRepositoryMock.Setup(repo => repo.ExistsAsync(
                It.IsAny<Specification<Room>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Room.RoomNumberAlreadyExists);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_SuccessfullyCreatesRoom_ReturnsSuccess(
        [Frozen] Mock<IRepository<Hotel>> hotelRepositoryMock,
        [Frozen] Mock<IRepository<Room>> roomRepositoryMock,
        [Frozen] Mock<ICudRepository<Room>> roomCudRepositoryMock,
        [Frozen] Mock<IImageRepository> imageRepositoryMock,
        [Frozen] Mock<IUnitOfWork> unitOfWorkMock,
        [Frozen] Mock<IMapper> mapperMock,
        CreateRoomCommand request,
        Room room,
        CreateRoomCommandHandler handler)
    {
        // Arrange
        hotelRepositoryMock.Setup(repo => repo.ExistsAsync(
                It.IsAny<Specification<Hotel>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        roomRepositoryMock.Setup(repo => repo.ExistsAsync(
                It.IsAny<Specification<Room>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        mapperMock.Setup(m => m.Map<Room>(request)).Returns(room);

        imageRepositoryMock.Setup(repo => repo.SaveAndSetAll(
            It.IsAny<IEnumerable<IFile>>(), room, It.IsAny<Expression<Func<Room, ICollection<Image>>>>()));

        roomCudRepositoryMock.Setup(repo => repo.Add(It.IsAny<Room>()));
        unitOfWorkMock.Setup(uow => uow.CommitAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(room.Id);
        roomCudRepositoryMock.Verify(repo => repo.Add(room), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}