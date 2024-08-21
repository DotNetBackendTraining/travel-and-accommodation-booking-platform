using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Features.Cities.Queries.CityDetails.Admin;

public class AdminCityDetailsQuery : IQuery<AdminCityDetailsResponse>
{
    public Guid Id { get; set; }
}