using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Users.Commands.LoginUser;
using TravelAccommodationBookingPlatform.Application.Users.Commands.RegisterUser;
using TravelAccommodationBookingPlatform.Presentation.Requests;

namespace TravelAccommodationBookingPlatform.Presentation.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<LoginUserRequest, LoginUserCommand>();
        CreateMap<RegisterUserRequest, RegisterUserCommand>();
    }
}