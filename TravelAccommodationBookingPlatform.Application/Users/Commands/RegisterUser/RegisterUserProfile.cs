using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Users.Commands.RegisterUser;

public class RegisterUserProfile : Profile
{
    public RegisterUserProfile()
    {
        CreateMap<RegisterUserCommand, User>();
    }
}