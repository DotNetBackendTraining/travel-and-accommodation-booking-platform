using FluentAssertions;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;
using TravelAccommodationBookingPlatform.TestsCommon.Customizations;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Specifications.HotelSearch;

public class HotelSearchSpecificationTests
{
    [Theory, AutoMoqData(omitOnRecursion: true, [typeof(RoomWithoutBookingsCustomization)])]
    public void ShouldFilterCorrectly_WhenAllCriteriaAreMet(
        Hotel hotel)
    {
        // Arrange
        var filters = new HotelSearchQuery.HotelSearchFilters
        {
            SearchTerm = hotel.Name,
            General =
            {
                NumberOfGuests = hotel.Rooms.First().MaxNumberOfGuests,
                Rooms = hotel.Rooms.Count
            },
            Advanced =
            {
                AllowedStarRatings = [hotel.StarRate],
                RequiredAmenities = hotel.Amenities.ToList()
            }
        };

        // Act
        var spec = new HotelSearchSpecification(filters, HotelSearchQuery.SortingOption.Name);
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().ContainSingle();
        result.First().Id.Should().Be(hotel.Id);
    }

    [Theory, AutoMoqData(omitOnRecursion: true, [typeof(RoomWithoutBookingsCustomization)])]
    public void ShouldFilterCorrectly_WithPartialCriteria(
        List<Room> rooms,
        Hotel hotel)
    {
        // Arrange
        var filters = new HotelSearchQuery.HotelSearchFilters
        {
            SearchTerm = hotel.Name, // match
            Advanced =
            {
                RequiredRoomTypes = [RoomType.Boutique] // doesn't match
            }
        };

        rooms.ForEach(r => r.RoomType = RoomType.Luxury);
        hotel.Rooms = rooms;

        // Act
        var spec = new HotelSearchSpecification(filters, HotelSearchQuery.SortingOption.Name);
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().BeEmpty();
    }

    [Theory, AutoMoqData(omitOnRecursion: true, [typeof(RoomWithoutBookingsCustomization)])]
    public void HotelSearchSpecification_ShouldApplySortingOption(
        List<Hotel> hotels)
    {
        // Arrange
        var filters = new HotelSearchQuery.HotelSearchFilters { SearchTerm = "Hotel" }; // Match all hotels
        hotels.ForEach(h =>
        {
            h.Name = $"Hotel {Guid.NewGuid().ToString()[..8]}"; // Ensure unique and comparable names
        });

        // Act
        var spec = new HotelSearchSpecification(filters, HotelSearchQuery.SortingOption.Name);
        var result = spec.Evaluate(hotels.AsQueryable()).ToList();

        // Assert Success
        result.Should().BeInAscendingOrder(h => h.Name);
        result.Count.Should().Be(hotels.Count);
    }
}