using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Auth;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}