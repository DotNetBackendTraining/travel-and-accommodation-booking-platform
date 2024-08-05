using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Shared.Pagination;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingSearch;

public class BookingSearchQuery : IQuery<BookingSearchResponse>
{
    public Guid UserId { get; set; }
    public PaginationParameters PaginationParameters { get; set; } = default!;
    public BookingSearchFilters Filters { get; set; } = new();

    public class BookingSearchFilters
    {
        public TimespanOption Timespan { get; set; } = TimespanOption.All;
        public bool LatestBookingPerHotel { get; set; }
        public DateTime? CheckingInStartDate { get; set; }
        public DateTime? CheckingInEndDate { get; set; }
        public DateTime? CheckingOutStartDate { get; set; }
        public DateTime? CheckingOutEndDate { get; set; }
    }

    public enum TimespanOption
    {
        All = 0,
        PastOnly = 1,
        FutureOnly = 2
    }
}