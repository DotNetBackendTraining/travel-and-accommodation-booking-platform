using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Commands.CreateHotel;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelDetails;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelDetails.Admin;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch.Dtos;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Hotels.Requests;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Hotels.ViewModels;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Hotels;

public class HotelProfile : Profile
{
    public HotelProfile()
    {
        // CreateHotel
        CreateMap<CreateHotelRequest, CreateHotelCommand>();

        // HotelSearch
        CreateMap<HotelSearchResult.HotelSummary, HotelSearchViewModel.HotelSearchResult>();
        CreateMap<HotelSearchResult.HotelPriceDeal, HotelSearchViewModel.HotelSearchResult>();
        CreateMap<HotelSearchResult, HotelSearchViewModel.HotelSearchResult>()
            .IncludeMembers(src => src.Summary, src => src.PriceDeal);
        CreateMap<AvailableFiltersResult, HotelSearchViewModel.AvailableFiltersResult>();
        CreateMap<HotelSearchResponse, HotelSearchViewModel>();

        // HotelDetails
        CreateMap<HotelDetailsResponse, HotelDetailsViewModel>();
        CreateMap<HotelDetailsResponse, AdminHotelDetailsViewModel>();
        CreateMap<AdminHotelDetailsResponse, AdminHotelDetailsViewModel>()
            .IncludeMembers(src => src.HotelDetails);
    }
}