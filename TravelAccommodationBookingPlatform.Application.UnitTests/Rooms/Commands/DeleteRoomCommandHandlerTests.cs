using Ardalis.Specification;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.Rooms.Commands.DeleteRoom;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Rooms.Commands;

public class DeleteRoomCommandHandlerTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_RoomNotFound_ReturnsFailure(
        [Frozen] Mock<IRepository<Room>> roomRepositoryMock,
        DeleteRoomCommand request,
        DeleteRoomCommandHandler handler)
    {
        // Arrange
        roomRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Room>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Room?)null);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Room.IdNotFound);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_RoomHasBookings_ReturnsFailure(
        [Frozen] Mock<IRepository<Room>> roomRepositoryMock,
        Room room,
        DeleteRoomCommand request,
        DeleteRoomCommandHandler handler)
    {
        // Arrange
        roomRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Room>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(room);

        roomRepositoryMock.Setup(repo => repo.ExistsAsync(
                It.IsAny<Specification<Room>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Room.CannotDeleteRoomWithBookings);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_SuccessfullyDeletesRoom_ReturnsSuccess(
        [Frozen] Mock<IRepository<Room>> roomRepositoryMock,
        [Frozen] Mock<ICudRepository<Room>> cudRoomRepositoryMock,
        [Frozen] Mock<IImageRepository> imageRepositoryMock,
        [Frozen] Mock<IUnitOfWork> unitOfWorkMock,
        Room room,
        DeleteRoomCommand request,
        DeleteRoomCommandHandler handler)
    {
        // Arrange
        roomRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Room>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(room);

        roomRepositoryMock.Setup(repo => repo.ExistsAsync(
                It.IsAny<Specification<Room>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        imageRepositoryMock.Setup(repo => repo.DeleteAll(It.IsAny<List<Image>>()));
        cudRoomRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Room>()));
        unitOfWorkMock.Setup(uow => uow.CommitAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        cudRoomRepositoryMock.Verify(repo => repo.Delete(room), Times.Once);
        imageRepositoryMock.Verify(repo => repo.DeleteAll(It.IsAny<List<Image>>()), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}