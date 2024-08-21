using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Moq;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch.Dtos;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch.Specifications;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Specifications.HotelSearch;

public class HotelSearchResultSpecificationTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void HotelSearchResultsSpecification_ShouldReturnCorrectlyMappedResults(
        Hotel hotel,
        Mock<IMapper> mapper,
        HotelSearchOptions options)
    {
        // Arrange
        var hotels = new List<Hotel> { hotel };
        var filters = new HotelSearchFilters();

        // Act
        var spec = new HotelSearchResultSpecification(filters, options, mapper.Object);
        var result = spec.Evaluate(hotels.AsQueryable()).ToList();

        // Assert
        result.Should().HaveCount(1);
        var hotelResult = result.First();
        hotelResult.Summary.Should().NotBeNull();
        mapper.Verify(m => m.Map<HotelSearchResult.HotelSummary>(hotel), Times.Once);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void HotelSearchResultsSpecification_ShouldIncludePriceDeal_WhenAvailable(
        Hotel hotel,
        [Frozen] Discount discount,
        IMapper mapper,
        HotelSearchOptions options)
    {
        // Arrange
        options.IncludePriceDealIfAvailable = true;
        hotel.ActiveDiscount = discount;
        hotel.Rooms.ToList().ForEach(r => r.Price = new Price { Value = new Random().Next(100, 500) });

        var hotels = new List<Hotel> { hotel };
        var filters = new HotelSearchFilters();

        // Act
        var spec = new HotelSearchResultSpecification(filters, options, mapper);
        var result = spec.Evaluate(hotels.AsQueryable()).ToList();

        // Assert
        result.Should().HaveCount(1);
        var hotelResult = result.First();
        hotelResult.PriceDeal.Should().NotBeNull();
        hotelResult.PriceDeal.MinimumPriceDeal.Should().NotBeNull();
        hotelResult.PriceDeal.MaximumPriceDeal.Should().NotBeNull();
        hotelResult.PriceDeal.MinimumPriceDeal.OriginalPrice.Value.Should().Be(hotel.Rooms.Min(r => r.Price.Value));
        hotelResult.PriceDeal.MaximumPriceDeal.OriginalPrice.Value.Should().Be(hotel.Rooms.Max(r => r.Price.Value));
        hotelResult.PriceDeal.MinimumPriceDeal.AppliedDiscount.Should().Be(discount);
        hotelResult.PriceDeal.MaximumPriceDeal.AppliedDiscount.Should().Be(discount);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void HotelSearchResultsSpecification_ShouldNotIncludePriceDeal_WhenNotAvailable(
        Hotel hotel,
        IMapper mapper,
        HotelSearchOptions options)
    {
        // Arrange
        options.IncludePriceDealIfAvailable = false;
        hotel.Rooms.ToList().ForEach(r => r.Price = new Price { Value = new Random().Next(100, 500) });

        var hotels = new List<Hotel> { hotel };
        var filters = new HotelSearchFilters();

        // Act
        var spec = new HotelSearchResultSpecification(filters, options, mapper);
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
        HotelSearchOptions options)
    {
        // Arrange
        options.IncludePriceDealIfAvailable = true;
        hotel.Rooms.Clear(); // No rooms in the hotel
        var hotels = new List<Hotel> { hotel };
        var filters = new HotelSearchFilters();

        // Act
        var spec = new HotelSearchResultSpecification(filters, options, mapper);
        var result = spec.Evaluate(hotels.AsQueryable()).ToList();

        // Assert
        result.Should().HaveCount(1);
        var hotelResult = result.First();
        hotelResult.PriceDeal.Should().BeNull();
    }
}