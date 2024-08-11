using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingSearch;
using TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingSearch.DTOs;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Bookings.Requests;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Bookings;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<BookingSearchRequest, BookingSearchFilters>();
        CreateMap<BookingSearchRequest, BookingSearchFilters>();
        CreateMap<BookingSearchRequest, BookingSearchQuery>()
            .ForMember(dst => dst.Filters, opt => opt.MapFrom(src => src));
    }
}