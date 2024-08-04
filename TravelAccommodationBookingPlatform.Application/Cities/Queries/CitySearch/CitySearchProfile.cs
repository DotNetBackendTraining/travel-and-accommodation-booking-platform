using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Cities.Queries.CitySearch;

public class CitySearchProfile : Profile
{
    public CitySearchProfile()
    {
        CreateMap<City, CitySearchResponse.CitySearchResult>();
    }
}