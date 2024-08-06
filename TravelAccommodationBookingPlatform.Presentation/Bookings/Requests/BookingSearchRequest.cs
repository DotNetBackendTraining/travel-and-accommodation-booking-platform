using TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingSearch;
using TravelAccommodationBookingPlatform.Application.Shared;

namespace TravelAccommodationBookingPlatform.Presentation.Bookings.Requests;

public class BookingSearchRequest
{
    public PaginationParameters PaginationParameters { get; set; } = default!;

    /// <summary>
    /// The timespan option to filter bookings by their time period. Options:
    /// <list type="bullet">
    /// <item>
    /// <description>All - Includes all bookings regardless of time period.</description>
    /// </item>
    /// <item>
    /// <description>PastOnly - Includes only past bookings.</description>
    /// </item>
    /// <item>
    /// <description>FutureOnly - Includes only future bookings.</description>
    /// </item>
    /// </list>
    /// </summary>
    public BookingSearchQuery.TimespanOption Timespan { get; set; } = BookingSearchQuery.TimespanOption.All;

    /// <summary>
    /// Indicates whether to retrieve only the latest booking per hotel. If set to true,
    /// only the latest booking per hotel will be retrieved based on the timespan filter.
    /// So if PastOnly timespan is set, only the latest booking in the past for each hotel is retrieved.
    /// </summary>
    public bool LatestBookingPerHotel { get; set; }

    /// <summary>
    /// The start date for filtering bookings by their check-in date.
    /// </summary>
    public DateTime? CheckingInStartDate { get; set; }

    /// <summary>
    /// The end date for filtering bookings by their check-in date.
    /// </summary>
    public DateTime? CheckingInEndDate { get; set; }

    /// <summary>
    /// The start date for filtering bookings by their check-out date.
    /// </summary>
    public DateTime? CheckingOutStartDate { get; set; }

    /// <summary>
    /// The end date for filtering bookings by their check-out date.
    /// </summary>
    public DateTime? CheckingOutEndDate { get; set; }
}