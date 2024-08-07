using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Moq;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Specifications.HotelSearch;

public class HotelSearchResultsSpecificationTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void HotelSearchResultsSpecification_ShouldReturnCorrectlyMappedResults(
        Hotel hotel,
        Mock<IMapper> mapper,
        HotelSearchQuery.HotelSearchOptions options)
    {
        // Arrange
        var hotels = new List<Hotel> { hotel };
        var filters = new HotelSearchQuery.HotelSearchFilters();

        // Act
        var spec = new HotelSearchResultsSpecification(filters, options, mapper.Object);
        var result = spec.Evaluate(hotels.AsQueryable()).ToList();

        // Assert
        result.Should().HaveCount(1);
        var hotelResult = result.First();
        hotelResult.HotelSummary.Should().NotBeNull();
        mapper.Verify(m => m.Map<HotelSearchResponse.HotelSummary>(hotel), Times.Once);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void HotelSearchResultsSpecification_ShouldIncludePriceDeal_WhenAvailable(
        Hotel hotel,
        [Frozen] DiscountRate discountRate,
        IMapper mapper,
        HotelSearchQuery.HotelSearchOptions options)
    {
        // Arrange
        options.IncludePriceDealIfAvailable = true;
        hotel.ActiveDiscount = new Discount { Rate = discountRate };
        hotel.Rooms.ToList().ForEach(r => r.Price = new Price { Value = new Random().Next(100, 500) });

        var hotels = new List<Hotel> { hotel };
        var filters = new HotelSearchQuery.HotelSearchFilters();

        // Act
        var spec = new HotelSearchResultsSpecification(filters, options, mapper);
        var result = spec.Evaluate(hotels.AsQueryable()).ToList();

        // Assert
        result.Should().HaveCount(1);
        var hotelResult = result.First();
        hotelResult.PriceDeal.Should().NotBeNull();
        hotelResult.PriceDeal.MinimumPriceDeal.Should().NotBeNull();
        hotelResult.PriceDeal.MaximumPriceDeal.Should().NotBeNull();
        hotelResult.PriceDeal.MinimumPriceDeal.OriginalPrice.Value.Should().Be(hotel.Rooms.Min(r => r.Price.Value));
        hotelResult.PriceDeal.MaximumPriceDeal.OriginalPrice.Value.Should().Be(hotel.Rooms.Max(r => r.Price.Value));
        hotelResult.PriceDeal.MinimumPriceDeal.DiscountRate.Should().Be(discountRate);
        hotelResult.PriceDeal.MaximumPriceDeal.DiscountRate.Should().Be(discountRate);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void HotelSearchResultsSpecification_ShouldNotIncludePriceDeal_WhenNotAvailable(
        Hotel hotel,
        IMapper mapper,
        HotelSearchQuery.HotelSearchOptions options)
    {
        // Arrange
        options.IncludePriceDealIfAvailable = false;
        hotel.Rooms.ToList().ForEach(r => r.Price = new Price { Value = new Random().Next(100, 500) });

        var hotels = new List<Hotel> { hotel };
        var filters = new HotelSearchQuery.HotelSearchFilters();

        // Act
        var spec = new HotelSearchResultsSpecification(filters, options, mapper);
        var result = spec.Evaluate(hotels.AsQueryable()).ToList();

        // Assert
        result.Should().HaveCount(1);
        var hotelResult = result.First();
        hotelResult.PriceDeal.Should().BeNull();
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void HotelSearchResultsSpecification_ShouldHandleEmptyRoomsList(
        Hotel hotel,
        IMapper mapper,
        HotelSearchQuery.HotelSearchOptions options)
    {
        // Arrange
        options.IncludePriceDealIfAvailable = true;
        hotel.Rooms.Clear(); // No rooms in the hotel
        var hotels = new List<Hotel> { hotel };
        var filters = new HotelSearchQuery.HotelSearchFilters();

        // Act
        var spec = new HotelSearchResultsSpecification(filters, options, mapper);
        var result = spec.Evaluate(hotels.AsQueryable()).ToList();

        // Assert
        result.Should().HaveCount(1);
        var hotelResult = result.First();
        hotelResult.PriceDeal.Should().BeNull();
    }
}