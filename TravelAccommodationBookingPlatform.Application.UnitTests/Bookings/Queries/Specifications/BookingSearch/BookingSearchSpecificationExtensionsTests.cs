using FluentAssertions;
using TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingSearch;
using TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingSearch.SpecificationExtensions;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;
using TravelAccommodationBookingPlatform.TestsCommon.Customizations;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Bookings.Queries.Specifications.BookingSearch;

public class BookingSearchSpecificationExtensionsTests
{
    [Theory, AutoMoqData(omitOnRecursion: true, typeof(CheckingDatesCustomization))]
    public void ApplyBookingSearchFilters_ShouldFilterCorrectly_WhenCriteriaAreMet(
        BookingSearchQuery.BookingSearchFilters filters,
        List<Booking> bookings,
        Guid userId)
    {
        // Arrange
        bookings.ForEach(b => b.UserId = userId);
        filters.CheckingInStartDate = DateTime.UtcNow.AddDays(-10);
        filters.CheckingInEndDate = DateTime.UtcNow.AddDays(10);
        filters.CheckingOutStartDate = DateTime.UtcNow.AddDays(-5);
        filters.CheckingOutEndDate = DateTime.UtcNow.AddDays(15);
        filters.Timespan = BookingSearchQuery.TimespanOption.All;
        filters.LatestBookingPerHotel = false;

        // Act
        var spec = new TestSpec<Booking>(q => q.ApplyBookingSearchFilters(filters, userId));
        var result = spec.Evaluate(bookings.AsQueryable()).ToList();

        // Assert
        result.All(b =>
            b.Checking.CheckInDate >= filters.CheckingInStartDate &&
            b.Checking.CheckInDate <= filters.CheckingInEndDate &&
            b.Checking.CheckOutDate >= filters.CheckingOutStartDate &&
            b.Checking.CheckOutDate <= filters.CheckingOutEndDate
        ).Should().BeTrue();
    }

    [Theory, AutoMoqData(omitOnRecursion: true, typeof(RoomsWithCommonHotelCustomization))]
    public void ApplyLatestPerHotelFilter_ShouldFilterCorrectly_WhenPastTimespanAndLatestBookingPerHotelIsTrue(
        Booking booking1,
        Booking booking2,
        Booking booking3,
        Booking booking4,
        Guid userId)
    {
        // Arrange
        var bookings = new List<Booking> { booking1, booking2, booking3, booking4 };
        bookings.ForEach(b => b.UserId = userId);

        var now = DateTime.UtcNow;
        bookings[0].Checking.CheckInDate = now.AddDays(-20);
        bookings[1].Checking.CheckInDate = now.AddDays(-10);
        bookings[2].Checking.CheckInDate = now.AddDays(-5); // latest booking in the past
        bookings[3].Checking.CheckInDate = now.AddDays(5);
        foreach (var booking in bookings)
        {
            booking.Checking.CheckOutDate = booking.Checking.CheckInDate.AddDays(1);
        }

        // Randomly assign each room to a booking
        var rooms = booking1.Rooms.First().Hotel.Rooms.ToList();
        rooms.ForEach(r => r.Bookings = bookings.ToList());

        var filters = new BookingSearchQuery.BookingSearchFilters
        {
            Timespan = BookingSearchQuery.TimespanOption.PastOnly,
            LatestBookingPerHotel = true
        };

        // Act
        var spec = new TestSpec<Booking>(q => q.ApplyBookingSearchFilters(filters, userId));
        var result = spec.Evaluate(bookings.AsQueryable()).ToList();

        // Assert
        result.Should().ContainSingle();
        result.First().Id.Should().Be(bookings[2].Id);
    }

    [Theory, AutoMoqData(omitOnRecursion: true, typeof(RoomsWithCommonHotelCustomization))]
    public void ApplyLatestPerHotelFilter_ShouldFilterCorrectly_WhenFutureTimespanAndLatestBookingPerHotelIsTrue(
        Booking booking1,
        Booking booking2,
        Booking booking3,
        Booking booking4,
        Guid userId)
    {
        // Arrange
        var bookings = new List<Booking> { booking1, booking2, booking3, booking4 };
        bookings.ForEach(b => b.UserId = userId);

        var now = DateTime.UtcNow;
        bookings[0].Checking.CheckInDate = now.AddDays(-5);
        bookings[1].Checking.CheckInDate = now.AddDays(10);
        bookings[2].Checking.CheckInDate = now.AddDays(15);
        bookings[3].Checking.CheckInDate = now.AddDays(20); // latest booking in the future
        foreach (var booking in bookings)
        {
            booking.Checking.CheckOutDate = booking.Checking.CheckInDate.AddDays(1);
        }

        // Randomly assign each room to a booking
        var rooms = booking1.Rooms.First().Hotel.Rooms.ToList();
        rooms.ForEach(r => r.Bookings = bookings.ToList());

        var filters = new BookingSearchQuery.BookingSearchFilters
        {
            Timespan = BookingSearchQuery.TimespanOption.FutureOnly,
            LatestBookingPerHotel = true
        };

        // Act
        var spec = new TestSpec<Booking>(q => q.ApplyBookingSearchFilters(filters, userId));
        var result = spec.Evaluate(bookings.AsQueryable()).ToList();

        // Assert
        result.Should().ContainSingle();
    }

    [Theory, AutoMoqData(omitOnRecursion: true, typeof(CheckingDatesCustomization))]
    public void ApplyCheckingInDateRangeFilter_ShouldFilterCorrectly(
        List<Booking> bookings)
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(-5);
        var endDate = DateTime.UtcNow.AddDays(5);

        // Act
        var spec = new TestSpec<Booking>(q => q.ApplyCheckingInDateRangeFilter(startDate, endDate));
        var result = spec.Evaluate(bookings.AsQueryable()).ToList();

        // Assert
        result.All(b =>
            b.Checking.CheckInDate >= startDate &&
            b.Checking.CheckInDate <= endDate
        ).Should().BeTrue();
    }

    [Theory, AutoMoqData(omitOnRecursion: true, typeof(CheckingDatesCustomization))]
    public void ApplyCheckingOutDateRangeFilter_ShouldFilterCorrectly(
        List<Booking> bookings)
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(-5);
        var endDate = DateTime.UtcNow.AddDays(5);

        // Act
        var spec = new TestSpec<Booking>(q => q.ApplyCheckingOutDateRangeFilter(startDate, endDate));
        var result = spec.Evaluate(bookings.AsQueryable()).ToList();

        // Assert
        result.All(b =>
            b.Checking.CheckOutDate >= startDate &&
            b.Checking.CheckOutDate <= endDate
        ).Should().BeTrue();
    }

    [Theory, AutoMoqData(omitOnRecursion: true, typeof(CheckingDatesCustomization))]
    public void ApplyTimespanFilter_ShouldFilterCorrectly_WhenTimespanIsPastOnly(
        List<Booking> bookings)
    {
        // Arrange
        var now = DateTime.UtcNow;
        bookings.ForEach(b => b.Checking.CheckOutDate = now.AddDays(-1));

        // Act
        var spec = new TestSpec<Booking>(q => q.ApplyTimespanFilter(BookingSearchQuery.TimespanOption.PastOnly));
        var result = spec.Evaluate(bookings.AsQueryable()).ToList();

        // Assert
        result.Should().NotBeEmpty();
        result.All(b => b.Checking.CheckOutDate < now).Should().BeTrue();
    }

    [Theory, AutoMoqData(omitOnRecursion: true, typeof(CheckingDatesCustomization))]
    public void ApplyTimespanFilter_ShouldFilterCorrectly_WhenTimespanIsFutureOnly(
        List<Booking> bookings)
    {
        // Arrange
        var now = DateTime.UtcNow;
        bookings.ForEach(b => b.Checking.CheckInDate = now.AddDays(1));

        // Act
        var spec = new TestSpec<Booking>(q => q.ApplyTimespanFilter(BookingSearchQuery.TimespanOption.FutureOnly));
        var result = spec.Evaluate(bookings.AsQueryable()).ToList();

        // Assert
        result.Should().NotBeEmpty();
        result.All(b => b.Checking.CheckInDate > now).Should().BeTrue();
    }

    [Theory, AutoMoqData(omitOnRecursion: true, typeof(CheckingDatesCustomization))]
    public void ApplyTimespanFilter_ShouldHandleAllTimespanOption(
        List<Booking> bookings)
    {
        // Act
        var spec = new TestSpec<Booking>(q => q.ApplyTimespanFilter(BookingSearchQuery.TimespanOption.All));
        var result = spec.Evaluate(bookings.AsQueryable()).ToList();

        // Assert
        result.Should().HaveCount(bookings.Count);
    }
}