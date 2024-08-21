using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Features.Cities.Queries.CityDetails;

public class CityDetailsQuery : IQuery<CityDetailsResponse>
{
    public Guid Id { get; set; }
}