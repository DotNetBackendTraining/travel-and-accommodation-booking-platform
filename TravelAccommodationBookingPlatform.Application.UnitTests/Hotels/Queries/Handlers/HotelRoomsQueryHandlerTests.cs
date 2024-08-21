using Ardalis.Specification;
using AutoMapper;
using Moq;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelRooms;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Handlers;

public class HotelRoomsQueryHandlerTests :
    HotelQueryHandlerTestBase<HotelRoomsQueryHandler, HotelRoomsQuery, HotelRoomsResponse>
{
    protected override HotelRoomsQueryHandler CreateHandler(IRepository<Hotel> repository)
    {
        return new HotelRoomsQueryHandler(repository, Mock.Of<IMapper>());
    }

    protected override Task SetupRepositoryForFailure(Mock<IRepository<Hotel>> mockRepository)
    {
        mockRepository.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Hotel, HotelRoomsResponse>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((HotelRoomsResponse)null!);
        return Task.CompletedTask;
    }

    protected override Task SetupRepositoryForSuccess(Mock<IRepository<Hotel>> mockRepository,
        HotelRoomsResponse response)
    {
        mockRepository.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Hotel, HotelRoomsResponse>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        return Task.CompletedTask;
    }
}