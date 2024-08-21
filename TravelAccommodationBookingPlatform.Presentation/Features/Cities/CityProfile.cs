using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Features.Cities.Commands.CreateCity;
using TravelAccommodationBookingPlatform.Application.Features.Cities.Queries.CityDetails;
using TravelAccommodationBookingPlatform.Application.Features.Cities.Queries.CityDetails.Admin;
using TravelAccommodationBookingPlatform.Presentation.Features.Cities.Requests;
using TravelAccommodationBookingPlatform.Presentation.Features.Cities.ViewModels;

namespace TravelAccommodationBookingPlatform.Presentation.Features.Cities;

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