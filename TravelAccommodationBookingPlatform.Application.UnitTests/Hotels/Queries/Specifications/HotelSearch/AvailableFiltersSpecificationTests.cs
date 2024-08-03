using FluentAssertions;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Specifications.HotelSearch;

public class AvailableFiltersSpecificationTests
{
    private static HotelSearchResponse.AvailableFiltersResult? GetSpecResult(IEnumerable<Hotel> hotels)
    {
        var spec = new AvailableFiltersSpecification();
        return spec.Evaluate(hotels.GroupBy(_ => 1).AsQueryable()).FirstOrDefault();
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void ShouldReturnCorrectMinimumAndMaximumPrices(Hotel hotel)
    {
        // Arrange
        hotel.Rooms = new List<Room>
        {
            new() { Price = new Price { Value = 100 } },
            new() { Price = new Price { Value = 200 } }
        };

        // Act
        var result = GetSpecResult([hotel]);

        // Assert
        result.Should().NotBeNull();
        result.MinimumPrice.Value.Should().Be(100);
        result.MaximumPrice.Value.Should().Be(200);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void ShouldReturnDistinctOrderedAmenities(Hotel hotel)
    {
        // Arrange
        hotel.Amenities = [Amenity.WiFi, Amenity.Pool, Amenity.WiFi, Amenity.Gym];

        // Act
        var result = GetSpecResult([hotel]);

        // Assert
        result.Should().NotBeNull();
        result.AvailableAmenities.Should().BeEquivalentTo(
            [Amenity.WiFi, Amenity.Pool, Amenity.Gym],
            options => options.WithStrictOrdering());
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void ShouldReturnDistinctOrderedRoomTypes(
        Room room1,
        Room room2,
        Room room3,
        Room room4,
        Hotel hotel)
    {
        // Arrange
        room1.RoomType = RoomType.Luxury;
        room2.RoomType = RoomType.Boutique;
        room3.RoomType = RoomType.Budget;
        room4.RoomType = RoomType.Luxury;
        hotel.Rooms = [room1, room2, room3, room4];

        // Act
        var result = GetSpecResult([hotel]);

        // Assert
        result.Should().NotBeNull();
        result.AvailableRoomTypes.Should().BeEquivalentTo(
            [RoomType.Luxury, RoomType.Budget, RoomType.Boutique],
            options => options.WithStrictOrdering());
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void ShouldReturnDistinctOrderedStarRatings(
        Hotel hotel1,
        Hotel hotel2,
        Hotel hotel3,
        Hotel hotel4)
    {
        // Arrange
        hotel1.StarRate = StarRate.ThreeStars;
        hotel2.StarRate = StarRate.FiveStars;
        hotel3.StarRate = StarRate.FourStars;
        hotel4.StarRate = StarRate.FiveStars;

        // Act
        var result = GetSpecResult([hotel1, hotel2, hotel3, hotel4]);

        // Assert
        result.Should().NotBeNull();
        result.AvailableStarRatings.Should().BeEquivalentTo(
            [StarRate.ThreeStars, StarRate.FourStars, StarRate.FiveStars],
            options => options.WithStrictOrdering());
    }
}