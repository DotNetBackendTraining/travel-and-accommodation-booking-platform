using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Commands.CreateBooking;

public class CreateBookingProfile : Profile
{
    public CreateBookingProfile()
    {
        CreateMap<CreateBookingCommand, Booking>()
            .ForMember(dst => dst.Rooms, opt => opt.Ignore());
    }
}