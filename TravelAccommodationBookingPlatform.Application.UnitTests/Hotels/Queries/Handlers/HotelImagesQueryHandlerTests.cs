using Ardalis.Specification;
using Moq;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelImages;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Handlers;

public class HotelImagesQueryHandlerTests :
    HotelQueryHandlerTestBase<HotelImagesQueryHandler, HotelImagesQuery, HotelImagesResponse>
{
    protected override HotelImagesQueryHandler CreateHandler(IRepository<Hotel> repository)
    {
        return new HotelImagesQueryHandler(repository);
    }

    protected override Task SetupRepositoryForFailure(Mock<IRepository<Hotel>> mockRepository)
    {
        mockRepository.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Hotel, HotelImagesResponse>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((HotelImagesResponse)null!);
        return Task.CompletedTask;
    }

    protected override Task SetupRepositoryForSuccess(Mock<IRepository<Hotel>> mockRepository,
        HotelImagesResponse response)
    {
        mockRepository.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Hotel, HotelImagesResponse>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        return Task.CompletedTask;
    }
}