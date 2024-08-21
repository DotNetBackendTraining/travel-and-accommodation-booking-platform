using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserProfile : Profile
{
    public RegisterUserProfile()
    {
        CreateMap<RegisterUserCommand, User>();
    }
}