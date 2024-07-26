using Ardalis.Specification;
using Moq;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelReviews;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Handlers;

public class HotelReviewsQueryHandlerTests :
    HotelQueryHandlerTestBase<HotelReviewsQueryHandler, HotelReviewsQuery, HotelReviewsResponse>
{
    protected override HotelReviewsQueryHandler CreateHandler(IRepository<Hotel> repository)
    {
        return new HotelReviewsQueryHandler(repository);
    }

    protected override Task SetupRepositoryForFailure(Mock<IRepository<Hotel>> mockRepository)
    {
        mockRepository.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Hotel, HotelReviewsResponse>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((HotelReviewsResponse)null!);
        return Task.CompletedTask;
    }

    protected override Task SetupRepositoryForSuccess(Mock<IRepository<Hotel>> mockRepository,
        HotelReviewsResponse response)
    {
        mockRepository.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Hotel, HotelReviewsResponse>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        return Task.CompletedTask;
    }
}