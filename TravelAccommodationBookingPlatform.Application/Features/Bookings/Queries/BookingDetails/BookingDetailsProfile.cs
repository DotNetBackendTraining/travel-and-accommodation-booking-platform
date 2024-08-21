using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingDetails.DTOs;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingDetails;

public class BookingDetailsProfile : Profile
{
    public BookingDetailsProfile()
    {
        CreateMap<Booking, BookingDetailsResult>();
        CreateMap<Hotel, BookingDetailsHotelResult>();
        CreateMap<Room, BookingDetailsRoomResult>();
    }
}