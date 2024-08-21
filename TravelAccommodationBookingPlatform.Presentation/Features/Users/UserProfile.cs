using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Features.Users.Commands.LoginUser;
using TravelAccommodationBookingPlatform.Application.Features.Users.Commands.RegisterUser;
using TravelAccommodationBookingPlatform.Presentation.Features.Users.Requests;

namespace TravelAccommodationBookingPlatform.Presentation.Features.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<LoginUserRequest, LoginUserCommand>();
        CreateMap<RegisterUserRequest, RegisterUserCommand>();
    }
}