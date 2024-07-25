using Ardalis.Specification;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelImages;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.HotelImages;

public class HotelImagesQueryHandlerTests
{
    [Theory, AutoMoqData]
    public async Task Handle_ReturnsFailure_WhenHotelNotFound(
        [Frozen] Mock<IRepository<Hotel>> mockGenericRepository,
        HotelImagesQueryHandler handler,
        HotelImagesQuery query)
    {
        mockGenericRepository.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Hotel, HotelImagesResponse>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((HotelImagesResponse)null!);

        var result = await handler.Handle(query, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Hotel.IdNotFound);
    }

    [Theory, AutoMoqData]
    public async Task Handle_ReturnsSuccess_WithHotelImages_WhenHotelIsFound(
        [Frozen] Mock<IRepository<Hotel>> mockGenericRepository,
        HotelImagesResponse hotelImagesResponse,
        HotelImagesQuery query,
        HotelImagesQueryHandler handler)
    {
        mockGenericRepository.Setup(repo => repo.GetAsync(
                It.IsAny<Specification<Hotel, HotelImagesResponse>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(hotelImagesResponse);

        var result = await handler.Handle(query, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(hotelImagesResponse);
    }
}