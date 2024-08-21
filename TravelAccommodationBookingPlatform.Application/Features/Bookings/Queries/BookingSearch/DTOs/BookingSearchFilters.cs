namespace TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingSearch.DTOs;

public class BookingSearchFilters
{
    public TimespanOption Timespan { get; set; } = TimespanOption.All;
    public bool LatestBookingPerHotel { get; set; }
    public DateTime? CheckingInStartDate { get; set; }
    public DateTime? CheckingInEndDate { get; set; }
    public DateTime? CheckingOutStartDate { get; set; }
    public DateTime? CheckingOutEndDate { get; set; }

    public enum TimespanOption
    {
        All = 0,
        PastOnly = 1,
        FutureOnly = 2
    }
}