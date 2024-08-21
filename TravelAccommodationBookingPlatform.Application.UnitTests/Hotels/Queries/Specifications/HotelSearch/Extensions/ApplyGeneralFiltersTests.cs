using FluentAssertions;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch.Dtos;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch.Specifications.Extensions;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;
using TravelAccommodationBookingPlatform.TestsCommon.Customizations;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Specifications.HotelSearch.Extensions;

public class ApplyGeneralFiltersTests
{
    [Theory, AutoMoqData(omitOnRecursion: true, [typeof(RoomWithoutBookingsCustomization)])]
    public void ApplyGeneralFilters_ShouldFilterCorrectly_WhenCriteriaAreMet(
        HotelSearchFilters.GeneralFilters filters,
        List<Room> rooms,
        Hotel hotel)
    {
        // Arrange
        rooms.ForEach(r => r.MaxNumberOfGuests = new NumberOfGuests { Adults = 2, Children = 1 });
        hotel.Rooms = rooms;
        filters.Rooms = rooms.Count;
        filters.NumberOfGuests = new NumberOfGuests
        {
            Adults = rooms.Sum(r => r.MaxNumberOfGuests.Adults),
            Children = rooms.Sum(r => r.MaxNumberOfGuests.Children)
        };

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplyGeneralFilters(filters));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert Success
        result.Should().ContainSingle();
        result.First().Id.Should().Be(hotel.Id);
    }

    [Theory, AutoMoqData(omitOnRecursion: true, [typeof(RoomWithoutBookingsCustomization)])]
    public void ApplyGeneralFilters_ShouldFilterCorrectly_WhenRoomCountIsInsufficient(
        HotelSearchFilters.GeneralFilters filters,
        List<Room> rooms,
        Hotel hotel)
    {
        // Arrange
        rooms.ForEach(r => r.MaxNumberOfGuests = new NumberOfGuests { Adults = 2, Children = 1 });
        hotel.Rooms = rooms;
        filters.Rooms = rooms.Count + 1; // More than available

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplyGeneralFilters(filters));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().BeEmpty();
    }

    [Theory, AutoMoqData(omitOnRecursion: true, [typeof(RoomWithoutBookingsCustomization)])]
    public void ApplyGeneralFilters_ShouldFilterCorrectly_WhenGuestCountIsInsufficient(
        HotelSearchFilters.GeneralFilters filters,
        List<Room> rooms,
        Hotel hotel)
    {
        // Arrange
        rooms.ForEach(r => r.MaxNumberOfGuests = new NumberOfGuests { Adults = 2, Children = 1 });
        hotel.Rooms = rooms;
        filters.NumberOfGuests = new NumberOfGuests
        {
            Adults = rooms.Sum(r => r.MaxNumberOfGuests.Adults) + 1, // More than available
            Children = rooms.Sum(r => r.MaxNumberOfGuests.Children)
        };

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplyGeneralFilters(filters));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().BeEmpty();
    }

    [Theory, AutoMoqData(omitOnRecursion: true, [typeof(RoomWithoutBookingsCustomization)])]
    public void ApplyGeneralFilters_ShouldFilterCorrectly_WhenChildrenGuestCountIsInsufficient(
        HotelSearchFilters.GeneralFilters filters,
        List<Room> rooms,
        Hotel hotel)
    {
        // Arrange
        rooms.ForEach(r => r.MaxNumberOfGuests = new NumberOfGuests { Adults = 2, Children = 1 });
        hotel.Rooms = rooms;
        filters.NumberOfGuests = new NumberOfGuests
        {
            Adults = rooms.Sum(r => r.MaxNumberOfGuests.Adults),
            Children = rooms.Sum(r => r.MaxNumberOfGuests.Children) + 1 // More than available
        };

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplyGeneralFilters(filters));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().BeEmpty();
    }

    [Theory, AutoMoqData(omitOnRecursion: true, [typeof(RoomWithoutBookingsCustomization)])]
    public void ApplyGeneralFilters_ShouldHandleEmptyRoomsList(
        HotelSearchFilters.GeneralFilters filters,
        Hotel hotel)
    {
        // Arrange
        hotel.Rooms = new List<Room>();
        filters.Rooms = 1;

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplyGeneralFilters(filters));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().BeEmpty();
    }
}