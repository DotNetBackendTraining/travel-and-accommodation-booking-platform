using Ardalis.Specification;
using Moq;
using TravelAccommodationBookingPlatform.Application.Features.Rooms.Queries.RoomDetails;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Rooms.Queries.Handlers;

public class RoomDetailsQueryHandlerTests :
    RoomQueryHandlerTestBase<RoomDetailsQueryHandler, RoomDetailsQuery, RoomDetailsResponse>
{
    protected override RoomDetailsQueryHandler CreateHandler(IRepository<Room> repository)
    {
        return new RoomDetailsQueryHandler(repository);
    }

    protected override Task SetupRepositoryForFailure(Mock<IRepository<Room>> mockRepository)
    {
        mockRepository.Setup(repo => repo
                .GetWithProjectionAsync<RoomDetailsResponse>(
                    It.IsAny<Specification<Room>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync((RoomDetailsResponse)null!);
        return Task.CompletedTask;
    }

    protected override Task SetupRepositoryForSuccess(Mock<IRepository<Room>> mockRepository,
        RoomDetailsResponse response)
    {
        mockRepository.Setup(repo => repo
                .GetWithProjectionAsync<RoomDetailsResponse>(
                    It.IsAny<Specification<Room>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        return Task.CompletedTask;
    }
}