using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Cities.Commands.CreateCity;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Cities.Requests;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Cities;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<CreateCityRequest, CreateCityCommand>();
    }
}