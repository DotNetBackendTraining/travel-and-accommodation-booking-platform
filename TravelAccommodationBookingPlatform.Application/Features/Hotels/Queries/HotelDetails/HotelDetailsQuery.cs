using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelDetails;

public class HotelDetailsQuery : IQuery<HotelDetailsResponse>
{
    public Guid Id { get; set; }
}