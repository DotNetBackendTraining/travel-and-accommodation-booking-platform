using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingSearch;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Bookings.Requests;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Bookings;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<BookingSearchRequest, BookingSearchQuery.BookingSearchFilters>();
        CreateMap<BookingSearchRequest, BookingSearchQuery>()
            .ForMember(dst => dst.Filters, opt => opt.MapFrom(src => src));
    }
}