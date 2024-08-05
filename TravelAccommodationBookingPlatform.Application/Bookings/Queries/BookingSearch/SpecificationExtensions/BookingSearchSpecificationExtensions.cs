using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingSearch.SpecificationExtensions;

public static class BookingSearchSpecificationExtensions
{
    public static ISpecificationBuilder<Booking> ApplyBookingSearchFilters(
        this ISpecificationBuilder<Booking> query,
        BookingSearchQuery.BookingSearchFilters filters,
        Guid userId)
    {
        return query
            .ApplyTimespanFilter(filters.Timespan)
            .ApplyCheckingInDateRangeFilter(filters.CheckingInStartDate, filters.CheckingInEndDate)
            .ApplyCheckingOutDateRangeFilter(filters.CheckingOutStartDate, filters.CheckingOutEndDate)
            .ApplyLatestPerHotelFilter(filters.LatestBookingPerHotel, filters.Timespan, userId);
    }

    public static ISpecificationBuilder<Booking> ApplyLatestPerHotelFilter(
        this ISpecificationBuilder<Booking> query,
        bool latestPerHotel,
        BookingSearchQuery.TimespanOption timespan,
        Guid userId)
    {
        var now = DateTime.UtcNow;
        if (latestPerHotel)
        {
            query.Where(b =>
                !b.Rooms.First().Hotel.Rooms
                    // Select all user's bookings for the hotel of this booking
                    .SelectMany(r => r.Bookings)
                    .Any(otherBooking =>
                        otherBooking.UserId == userId &&
                        // Consider the right timespan only for bookings
                        (timespan != BookingSearchQuery.TimespanOption.PastOnly ||
                         otherBooking.Checking.CheckOutDate < now) &&
                        (timespan != BookingSearchQuery.TimespanOption.FutureOnly ||
                         otherBooking.Checking.CheckInDate > now) &&
                        // This booking should be the latest booking among them
                        otherBooking.Checking.CheckInDate > b.Checking.CheckInDate));
        }

        return query;
    }

    public static ISpecificationBuilder<Booking> ApplyCheckingInDateRangeFilter(
        this ISpecificationBuilder<Booking> query,
        DateTime? startDate,
        DateTime? endDate)
    {
        if (startDate.HasValue)
        {
            query.Where(b => b.Checking.CheckInDate >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query.Where(b => b.Checking.CheckInDate <= endDate.Value);
        }

        return query;
    }

    public static ISpecificationBuilder<Booking> ApplyCheckingOutDateRangeFilter(
        this ISpecificationBuilder<Booking> query,
        DateTime? startDate,
        DateTime? endDate)
    {
        if (startDate.HasValue)
        {
            query.Where(b => b.Checking.CheckOutDate >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query.Where(b => b.Checking.CheckOutDate <= endDate.Value);
        }

        return query;
    }

    public static ISpecificationBuilder<Booking> ApplyTimespanFilter(
        this ISpecificationBuilder<Booking> query,
        BookingSearchQuery.TimespanOption timespan)
    {
        var now = DateTime.UtcNow;
        switch (timespan)
        {
            case BookingSearchQuery.TimespanOption.PastOnly:
                query.Where(b => b.Checking.CheckOutDate < now);
                break;
            case BookingSearchQuery.TimespanOption.FutureOnly:
                query.Where(b => b.Checking.CheckInDate > now);
                break;
            case BookingSearchQuery.TimespanOption.All:
            default:
                break;
        }

        return query;
    }
}