using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context, IMapper mapper)
        : base(context, mapper)
    {
        _context = context;
    }

    public void AddUser(User user)
    {
        _context.Add(user);
    }
}