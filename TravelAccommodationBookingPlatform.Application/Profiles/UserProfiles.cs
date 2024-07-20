using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Users.Commands.RegisterUser;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Profiles;

public class UserProfiles : Profile
{
    public UserProfiles()
    {
        CreateMap<RegisterUserCommand, User>();
    }
}