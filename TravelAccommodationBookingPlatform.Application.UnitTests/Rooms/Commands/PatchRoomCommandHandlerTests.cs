using Ardalis.Specification;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using TravelAccommodationBookingPlatform.Application.Features.Rooms.Commands.PatchRoom;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Rooms.Commands;

public class PatchRoomCommandHandlerTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_RoomNotFound_ReturnsFailure(
        [Frozen] Mock<IRepository<Room>> roomRepositoryMock,
        PatchRoomCommand request,
        PatchRoomCommandHandler handler)
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
    public async Task Handle_ValidationFails_ThrowsValidationException(
        [Frozen] Mock<IRepository<Room>> roomRepositoryMock,
        [Frozen] Mock<IValidator<PatchRoomModel>> validatorMock,
        Room room,
        PatchRoomCommand request,
        PatchRoomCommandHandler handler)
    {
        // Arrange
        roomRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Room>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(room);

        validatorMock.Setup(v => v.ValidateAsync(
                It.IsAny<ValidationContext<PatchRoomModel>>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ValidationException(new[] { new ValidationFailure("Property", "Error") }));

        // Act
        Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_HotelNotFound_ReturnsFailure(
        [Frozen] Mock<IRepository<Room>> roomRepositoryMock,
        [Frozen] Mock<IRepository<Hotel>> hotelRepositoryMock,
        Room room,
        PatchRoomCommand request,
        PatchRoomCommandHandler handler)
    {
        // Arrange
        roomRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Room>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(room);

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
    public async Task Handle_SuccessfullyPatchesRoom_ReturnsSuccess(
        [Frozen] Mock<IRepository<Room>> roomRepositoryMock,
        [Frozen] Mock<IRepository<Hotel>> hotelRepositoryMock,
        [Frozen] Mock<ICudRepository<Room>> roomCudRepositoryMock,
        [Frozen] Mock<IUnitOfWork> unitOfWorkMock,
        [Frozen] Mock<IMapper> mapperMock,
        Room room,
        PatchRoomCommand request,
        PatchRoomCommandHandler handler)
    {
        // Arrange
        roomRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Room>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(room);

        hotelRepositoryMock.Setup(repo => repo.ExistsAsync(
                It.IsAny<Specification<Hotel>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        mapperMock.Setup(m => m.Map<PatchRoomModel>(room)).Returns(new PatchRoomModel());
        mapperMock.Setup(m => m.Map(It.IsAny<PatchRoomModel>(), room)).Returns(room);

        roomCudRepositoryMock.Setup(repo => repo.Update(It.IsAny<Room>()));
        unitOfWorkMock.Setup(uow => uow.CommitAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        roomCudRepositoryMock.Verify(repo => repo.Update(room), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}