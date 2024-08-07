using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Hotels.Commands.CreateHotel;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;
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
        CreateMap<HotelSearchResponse.HotelSummary, HotelSearchViewModel.HotelSearchResult>();
        CreateMap<HotelSearchResponse.HotelPriceDeal, HotelSearchViewModel.HotelSearchResult>();
        CreateMap<HotelSearchResponse.HotelSearchResult, HotelSearchViewModel.HotelSearchResult>()
            .IncludeMembers(src => src.HotelSummary, src => src.PriceDeal);
        CreateMap<HotelSearchResponse.AvailableFiltersResult, HotelSearchViewModel.AvailableFiltersResult>();
        CreateMap<HotelSearchResponse, HotelSearchViewModel>();
    }
}