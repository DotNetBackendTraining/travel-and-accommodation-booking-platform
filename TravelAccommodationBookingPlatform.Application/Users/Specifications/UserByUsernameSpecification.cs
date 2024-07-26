using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Users.Specifications;

public sealed class UserByUsernameSpecification : Specification<User>
{
    public UserByUsernameSpecification(string username)
    {
        Query.Where(u => u.Username == username);
    }
}