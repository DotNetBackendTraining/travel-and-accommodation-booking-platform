using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Hotels.Commands.CreateHotel;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.DTOs;
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
    }
}