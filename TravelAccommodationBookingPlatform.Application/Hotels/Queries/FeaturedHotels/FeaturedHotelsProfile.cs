using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.FeaturedHotels;

public class FeaturedHotelsProfile : Profile
{
    public FeaturedHotelsProfile()
    {
        CreateMap<Hotel, FeaturedHotelsResponse.FeaturedHotelSummary>()
            .ForMember(hs => hs.CityName, opt => opt.MapFrom(h => h.City.Name));
    }
}