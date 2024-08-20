using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelDetails.Admin;

public class AdminHotelDetailsQuery : IQuery<AdminHotelDetailsResponse>
{
    public Guid Id { get; set; }
}