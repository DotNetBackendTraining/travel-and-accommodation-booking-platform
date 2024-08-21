using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Users.Specifications;

public sealed class UserByEmailSpecification : Specification<User>
{
    public UserByEmailSpecification(string email)
    {
        Query.Where(u => u.Email == email);
    }
}