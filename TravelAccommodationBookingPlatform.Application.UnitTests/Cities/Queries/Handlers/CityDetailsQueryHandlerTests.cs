using Ardalis.Specification;
using Moq;
using TravelAccommodationBookingPlatform.Application.Cities.Queries.CityDetails;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Cities.Queries.Handlers;

public class CityDetailsQueryHandlerTests
    : CityQueryHandlerTestBase<CityDetailsQueryHandler, CityDetailsQuery, CityDetailsResponse>
{
    protected override CityDetailsQueryHandler CreateHandler(IRepository<City> repository)
    {
        return new CityDetailsQueryHandler(repository);
    }

    protected override Task SetupRepositoryForFailure(Mock<IRepository<City>> mockRepository)
    {
        mockRepository.Setup(repo => repo
                .GetWithProjectionAsync<CityDetailsResponse>(
                    It.IsAny<Specification<City>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync((CityDetailsResponse)null!);
        return Task.CompletedTask;
    }

    protected override Task SetupRepositoryForSuccess(Mock<IRepository<City>> mockRepository,
        CityDetailsResponse response)
    {
        mockRepository.Setup(repo => repo
                .GetWithProjectionAsync<CityDetailsResponse>(
                    It.IsAny<Specification<City>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        return Task.CompletedTask;
    }
}