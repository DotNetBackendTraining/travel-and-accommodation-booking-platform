using FluentAssertions;
using TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingSearch;
using TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingSearch.DTOs;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;
using TravelAccommodationBookingPlatform.TestsCommon.Customizations;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Bookings.Queries.Specifications.BookingSearch;

public class BookingSearchSpecificationTests
{
    [Theory, AutoMoqData(omitOnRecursion: true, typeof(RoomsWithCommonHotelCustomization))]
    public void BookingSearchSpecification_ShouldReturnCorrectlyFilteredAndSortedBookings(
        Booking booking1,
        Booking booking2,
        Booking booking3,
        Guid userId)
    {
        // Arrange
        var bookings = new List<Booking> { booking1, booking2, booking3 };
        bookings.ForEach(b => b.UserId = userId);

        var now = DateTime.UtcNow;
        booking1.Checking.CheckInDate = now.AddDays(-20);
        booking2.Checking.CheckInDate = now.AddDays(-10);
        booking3.Checking.CheckInDate = now.AddDays(-5);

        foreach (var booking in bookings)
        {
            booking.Checking.CheckOutDate = booking.Checking.CheckInDate.AddDays(1);
            var rooms = booking.Rooms.First().Hotel.Rooms.ToList();
            rooms.ForEach(r => r.Bookings = bookings.ToList());
        }

        // Act
        var spec = new BookingSearchSpecification(userId, new BookingSearchFilters());
        var result = spec.Evaluate(bookings.AsQueryable()).ToList();

        // Assert
        result.Should().HaveCount(bookings.Count);
        result.Should()
            .BeInDescendingOrder(b => b.Checking.CheckInDate)
            .And.ThenBeInDescendingOrder(b => b.Checking.CheckOutDate);
    }

    [Theory, AutoMoqData(omitOnRecursion: true, typeof(RoomsWithCommonHotelCustomization))]
    public void BookingSearchSpecification_ShouldReturnBookingsForSpecificUser(
        Booking booking1,
        Booking booking2,
        Booking booking3,
        Guid userId1,
        Guid userId2)
    {
        // Arrange
        var bookings = new List<Booking> { booking1, booking2, booking3 };
        booking1.UserId = userId1;
        booking2.UserId = userId2;
        booking3.UserId = userId1;

        var now = DateTime.UtcNow;
        booking1.Checking.CheckInDate = now.AddDays(-20);
        booking2.Checking.CheckInDate = now.AddDays(-10);
        booking3.Checking.CheckInDate = now.AddDays(-5);

        foreach (var booking in bookings)
        {
            booking.Checking.CheckOutDate = booking.Checking.CheckInDate.AddDays(1);
            var rooms = booking.Rooms.First().Hotel.Rooms.ToList();
            rooms.ForEach(r => r.Bookings = bookings.ToList());
        }

        // Act
        var spec = new BookingSearchSpecification(userId1, new BookingSearchFilters());
        var result = spec.Evaluate(bookings.AsQueryable()).ToList();

        // Assert
        result.Should().HaveCount(2);
    }

    [Theory, AutoMoqData(omitOnRecursion: true, typeof(RoomsWithCommonHotelCustomization))]
    public void BookingSearchSpecification_ShouldSelectCorrectFields(
        Booking booking,
        Guid userId)
    {
        // Arrange
        booking.UserId = userId;

        var now = DateTime.UtcNow;
        booking.Checking.CheckInDate = now.AddDays(-20);
        booking.Checking.CheckOutDate = booking.Checking.CheckInDate.AddDays(1);

        var rooms = booking.Rooms.First().Hotel.Rooms.ToList();
        rooms.ForEach(r => r.Bookings = new List<Booking> { booking });

        var expectedHotel = new
        {
            booking.Rooms.First().Hotel.Id,
            booking.Rooms.First().Hotel.Name,
            booking.Rooms.First().Hotel.StarRate,
            booking.Rooms.First().Hotel.ThumbnailImage,
            MinimumPrice = new Price { Value = rooms.Min(r => r.Price.Value) },
            MaximumPrice = new Price { Value = rooms.Max(r => r.Price.Value) }
        };

        // Act
        var spec = new BookingSearchSpecification(userId, new BookingSearchFilters());
        var result = spec.Evaluate(new List<Booking> { booking }.AsQueryable()).FirstOrDefault();

        // Assert
        result.Should().NotBeNull();
        result.Hotel.Should().NotBeNull();
        result.Hotel.Id.Should().Be(expectedHotel.Id);
        result.Hotel.Name.Should().Be(expectedHotel.Name);
        result.Hotel.StarRate.Should().Be(expectedHotel.StarRate);
        result.Hotel.ThumbnailImage.Should().Be(expectedHotel.ThumbnailImage);
        result.Hotel.MinimumPrice.Value.Should().Be(expectedHotel.MinimumPrice.Value);
        result.Hotel.MaximumPrice.Value.Should().Be(expectedHotel.MaximumPrice.Value);
    }
}