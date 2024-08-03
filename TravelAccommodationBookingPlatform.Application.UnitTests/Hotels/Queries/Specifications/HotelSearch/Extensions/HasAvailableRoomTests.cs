using FluentAssertions;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications.Extensions;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;
using TravelAccommodationBookingPlatform.TestsCommon.Customizations;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Specifications.HotelSearch.Extensions;

public class HasAvailableRoomTests
{
    [Theory, AutoMoqData(omitOnRecursion: true, [typeof(RoomWithoutBookingsCustomization)])]
    public void ShouldFilterCorrectly_WhenRoomIsAvailable(
        Checking checking,
        List<Room> rooms,
        Hotel hotel)
    {
        // Arrange
        var availableRoom = new Room { Bookings = new List<Booking>() };
        rooms.Add(availableRoom);
        hotel.Rooms = rooms;

        // Act
        var spec = new TestSpec<Hotel>(q => q.HasAvailableRoom(checking));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().ContainSingle();
        result.First().Id.Should().Be(hotel.Id);
    }

    [Theory, AutoMoqData(omitOnRecursion: true, [typeof(RoomWithoutBookingsCustomization)])]
    public void ShouldFilterCorrectly_WithOverlappingBookings(
        List<Room> rooms,
        Hotel hotel)
    {
        // Arrange
        var checking = new Checking
        {
            CheckInDate = DateTime.Now.AddDays(5),
            CheckOutDate = DateTime.Now.AddDays(10)
        };

        var conflictingBooking = new Booking
        {
            Checking = new Checking
            {
                CheckInDate = DateTime.Now.AddDays(8),
                CheckOutDate = DateTime.Now.AddDays(12)
            }
        };

        rooms.ForEach(r => r.Bookings = new List<Booking> { conflictingBooking });
        hotel.Rooms = rooms;

        // Act
        var spec = new TestSpec<Hotel>(q => q.HasAvailableRoom(checking));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().BeEmpty();
    }

    [Theory, AutoMoqData(omitOnRecursion: true, [typeof(RoomWithoutBookingsCustomization)])]
    public void ShouldFilterCorrectly_WithNonOverlappingBookings(
        List<Room> rooms,
        Hotel hotel)
    {
        // Arrange
        var checking = new Checking
        {
            CheckInDate = DateTime.Now.AddDays(15),
            CheckOutDate = DateTime.Now.AddDays(20)
        };

        var nonConflictingBooking = new Booking
        {
            Checking = new Checking
            {
                CheckInDate = DateTime.Now.AddDays(5),
                CheckOutDate = DateTime.Now.AddDays(10)
            }
        };

        rooms.ForEach(r => r.Bookings = new List<Booking> { nonConflictingBooking });
        hotel.Rooms = rooms;

        // Act
        var spec = new TestSpec<Hotel>(q => q.HasAvailableRoom(checking));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().ContainSingle();
        result.First().Id.Should().Be(hotel.Id);
    }
}