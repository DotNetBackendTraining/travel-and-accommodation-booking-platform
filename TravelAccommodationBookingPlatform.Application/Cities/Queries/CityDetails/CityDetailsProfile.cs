using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Cities.Queries.CityDetails;

public class CityDetailsProfile : Profile
{
    public CityDetailsProfile()
    {
        CreateMap<City, CityDetailsResponse>();
    }
}