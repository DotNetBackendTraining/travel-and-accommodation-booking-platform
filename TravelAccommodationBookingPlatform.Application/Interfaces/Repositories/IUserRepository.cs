using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    void AddUser(User user);
}