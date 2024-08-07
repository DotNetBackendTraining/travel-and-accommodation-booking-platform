using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Hotels.Commands.CreateHotel;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;
using TravelAccommodationBookingPlatform.Presentation.Hotels.Requests;
using TravelAccommodationBookingPlatform.Presentation.Hotels.ViewModels;

namespace TravelAccommodationBookingPlatform.Presentation.Hotels;

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