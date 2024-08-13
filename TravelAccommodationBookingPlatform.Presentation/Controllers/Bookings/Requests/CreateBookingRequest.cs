using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Bookings.Requests;

public class CreateBookingRequest
{
    /// <summary>
    /// A list of room IDs that the user wants to book.
    /// </summary>
    public List<Guid> RoomIds { get; set; } = default!;

    /// <summary>
    /// The checking details, including check-in and check-out dates.
    /// </summary>
    public Checking Checking { get; set; } = default!;

    /// <summary>
    /// The number of guests for the booking, including adults and children.
    /// </summary>
    public NumberOfGuests NumberOfGuests { get; set; } = default!;

    /// <summary>
    /// Any special requests the user may have for their stay.
    /// </summary>
    public SpecialRequest SpecialRequest { get; set; } = default!;
}