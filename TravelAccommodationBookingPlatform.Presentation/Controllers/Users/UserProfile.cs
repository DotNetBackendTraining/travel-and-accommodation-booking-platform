using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Users.Commands.LoginUser;
using TravelAccommodationBookingPlatform.Application.Users.Commands.RegisterUser;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Users.Requests;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<LoginUserRequest, LoginUserCommand>();
        CreateMap<RegisterUserRequest, RegisterUserCommand>();
    }
}