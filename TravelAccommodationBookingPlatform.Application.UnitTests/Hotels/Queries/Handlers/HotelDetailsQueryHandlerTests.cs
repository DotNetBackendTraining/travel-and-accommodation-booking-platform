using Ardalis.Specification;
using Moq;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelDetails;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Handlers;

public class HotelDetailsQueryHandlerTests :
    HotelQueryHandlerTestBase<HotelDetailsQueryHandler, HotelDetailsQuery, HotelDetailsResponse>
{
    protected override HotelDetailsQueryHandler CreateHandler(IRepository<Hotel> repository)
    {
        return new HotelDetailsQueryHandler(repository);
    }

    protected override Task SetupRepositoryForFailure(Mock<IRepository<Hotel>> mockRepository)
    {
        mockRepository.Setup(repo => repo
                .GetWithProjectionAsync<HotelDetailsResponse>(
                    It.IsAny<Specification<Hotel>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync((HotelDetailsResponse)null!);
        return Task.CompletedTask;
    }

    protected override Task SetupRepositoryForSuccess(Mock<IRepository<Hotel>> mockRepository,
        HotelDetailsResponse response)
    {
        mockRepository.Setup(repo => repo
                .GetWithProjectionAsync<HotelDetailsResponse>(
                    It.IsAny<Specification<Hotel>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        return Task.CompletedTask;
    }
}