using AutoFixture.Xunit2;
using FluentAssertions;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications.Extensions;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Specifications.HotelSearch.Extensions;

public class ApplyAdvancedFiltersTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void ShouldFilterCorrectly_WhenAllCriteriaAreMet(
        [Frozen] Price price,
        HotelSearchQuery.AdvancedFilters advancedFilters,
        Hotel hotel)
    {
        // Arrange
        hotel.Rooms = advancedFilters.RequiredRoomTypes?.Select(rt => new Room
        {
            Price = advancedFilters.MinPrice ?? new Price { Value = 100 },
            RoomType = rt
        }).ToList() ?? [];
        hotel.StarRate = advancedFilters.AllowedStarRatings?.FirstOrDefault() ?? StarRate.FiveStars;
        hotel.Amenities = advancedFilters.RequiredAmenities?.ToList() ?? [Amenity.WiFi];

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplyAdvancedFilters(advancedFilters));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().ContainSingle();
        result.First().Id.Should().Be(hotel.Id);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void ShouldFilterCorrectly_WhenMinPriceIsNotMet(
        [Frozen] Price price,
        HotelSearchQuery.AdvancedFilters advancedFilters,
        List<Room> rooms,
        Hotel hotel)
    {
        // Arrange
        advancedFilters.MinPrice = new Price { Value = 150 }; // more than any room price
        rooms.ForEach(r => r.Price = new Price { Value = 100 });
        hotel.Rooms = rooms;

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplyAdvancedFilters(advancedFilters));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().BeEmpty();
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void ShouldFilterCorrectly_WhenMaxPriceIsNotMet(
        [Frozen] Price price,
        HotelSearchQuery.AdvancedFilters advancedFilters,
        List<Room> rooms,
        Hotel hotel)
    {
        // Arrange
        advancedFilters.MaxPrice = new Price { Value = price.Value - 1 }; // less than any room price
        hotel.Rooms = rooms;

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplyAdvancedFilters(advancedFilters));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().BeEmpty();
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void ShouldFilterCorrectly_WhenStarRatingIsNotMet(
        [Frozen] Price price,
        HotelSearchQuery.AdvancedFilters advancedFilters,
        Hotel hotel)
    {
        // Arrange
        advancedFilters.AllowedStarRatings = [StarRate.ThreeStars];
        hotel.StarRate = StarRate.FiveStars;

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplyAdvancedFilters(advancedFilters));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().BeEmpty();
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void ShouldFilterCorrectly_WhenRequiredAmenitiesAreNotMet(
        [Frozen] Price price,
        HotelSearchQuery.AdvancedFilters advancedFilters,
        Hotel hotel)
    {
        // Arrange
        advancedFilters.RequiredAmenities = [Amenity.Pool];
        hotel.Amenities = [Amenity.WiFi];

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplyAdvancedFilters(advancedFilters));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().BeEmpty();
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void ShouldFilterCorrectly_WhenRequiredRoomTypesAreNotMet(
        [Frozen] Price price,
        HotelSearchQuery.AdvancedFilters advancedFilters,
        List<Room> rooms,
        Hotel hotel)
    {
        // Arrange
        advancedFilters.RequiredRoomTypes = [RoomType.Luxury];
        rooms.ForEach(r => r.RoomType = RoomType.Boutique);
        hotel.Rooms = rooms;

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplyAdvancedFilters(advancedFilters));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().BeEmpty();
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void ShouldHandleNullFilters(
        List<Room> rooms,
        Hotel hotel)
    {
        // Arrange
        var advancedFilters = new HotelSearchQuery.AdvancedFilters();
        hotel.Rooms = rooms;

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplyAdvancedFilters(advancedFilters));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().ContainSingle();
        result.First().Id.Should().Be(hotel.Id);
    }
}