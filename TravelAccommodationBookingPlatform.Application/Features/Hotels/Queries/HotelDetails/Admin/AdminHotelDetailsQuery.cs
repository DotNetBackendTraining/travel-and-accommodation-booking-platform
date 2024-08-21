using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelDetails.Admin;

public class AdminHotelDetailsQuery : IQuery<AdminHotelDetailsResponse>
{
    public Guid Id { get; set; }
}