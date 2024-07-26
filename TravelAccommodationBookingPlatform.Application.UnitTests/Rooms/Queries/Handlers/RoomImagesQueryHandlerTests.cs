using Ardalis.Specification;
using Moq;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.Rooms.Queries.RoomImages;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Rooms.Queries.Handlers;

public class RoomImagesQueryHandlerTests :
    RoomQueryHandlerTestBase<RoomImagesQueryHandler, RoomImagesQuery, RoomImagesResponse>
{
    protected override RoomImagesQueryHandler CreateHandler(IRepository<Room> repository)
    {
        return new RoomImagesQueryHandler(repository);
    }

    protected override Task SetupRepositoryForFailure(Mock<IRepository<Room>> mockRepository)
    {
        mockRepository.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Room, RoomImagesResponse>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((RoomImagesResponse)null!);
        return Task.CompletedTask;
    }

    protected override Task SetupRepositoryForSuccess(Mock<IRepository<Room>> mockRepository,
        RoomImagesResponse response)
    {
        mockRepository.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Room, RoomImagesResponse>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        return Task.CompletedTask;
    }
}