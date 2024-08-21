using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Features.Cities.Commands.CreateCity;
using TravelAccommodationBookingPlatform.Application.Features.Cities.Queries.CityDetails;
using TravelAccommodationBookingPlatform.Application.Features.Cities.Queries.CityDetails.Admin;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Cities.Requests;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Cities.ViewModels;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Cities;

public class CityProfile : Profile
{
    public CityProfile()
    {
        // CreateCity
        CreateMap<CreateCityRequest, CreateCityCommand>();

        // CityDetails
        CreateMap<CityDetailsResponse, CityDetailsViewModel>();
        CreateMap<CityDetailsResponse, AdminCityDetailsViewModel>();
        CreateMap<AdminCityDetailsResponse, AdminCityDetailsViewModel>()
            .IncludeMembers(src => src.CityDetails);
    }
}