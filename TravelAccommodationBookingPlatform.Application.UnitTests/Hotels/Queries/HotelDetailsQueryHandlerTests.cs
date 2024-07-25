using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelDetails;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.Shared;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries;

public class HotelDetailsQueryHandlerTests
{
    [Theory, AutoMoqData]
    public async Task Handle_ReturnsFailure_WhenHotelNotFound(
        [Frozen] Mock<IGenericRepository<Hotel>> mockGenericRepository,
        HotelDetailsQueryHandler handler,
        HotelDetailsQuery query)
    {
        mockGenericRepository.Setup(repo => repo
                .GetFirstBySpecificationAsync<HotelDetailsResponse>(
                    It.IsAny<Specification<Hotel>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync((HotelDetailsResponse)null!);

        var result = await handler.Handle(query, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Hotel.IdNotFound);
    }

    [Theory, AutoMoqData]
    public async Task Handle_ReturnsSuccess_WithHotelDetails_WhenHotelIsFound(
        [Frozen] Mock<IGenericRepository<Hotel>> mockGenericRepository,
        HotelDetailsResponse hotelDetailsResponse,
        HotelDetailsQuery query,
        HotelDetailsQueryHandler handler)
    {
        mockGenericRepository.Setup(repo =>
                repo.GetFirstBySpecificationAsync<HotelDetailsResponse>(
                    It.IsAny<Specification<Hotel>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(hotelDetailsResponse);

        var result = await handler.Handle(query, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(hotelDetailsResponse);
    }
}