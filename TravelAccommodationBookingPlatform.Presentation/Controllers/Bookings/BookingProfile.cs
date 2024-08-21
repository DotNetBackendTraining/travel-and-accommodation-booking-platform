using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Features.Bookings.Commands.CreateBooking;
using TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingSearch;
using TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingSearch.DTOs;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Bookings.Requests;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Bookings;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        // Create Booking
        CreateMap<CreateBookingRequest, CreateBookingCommand>();

        // Booking Search
        CreateMap<BookingSearchRequest, BookingSearchFilters>();
        CreateMap<BookingSearchRequest, BookingSearchFilters>();
        CreateMap<BookingSearchRequest, BookingSearchQuery>()
            .ForMember(dst => dst.Filters, opt => opt.MapFrom(src => src));
    }
}