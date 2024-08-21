using Ardalis.Specification;
using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelDetails.Admin;

public sealed class AdminHotelDetailsSpecification : Specification<Hotel, AdminHotelDetailsResponse>
{
    public AdminHotelDetailsSpecification(AdminHotelDetailsQuery query, IMapper mapper)
    {
        Query.Select(h => new AdminHotelDetailsResponse
            {
                HotelDetails = mapper.Map<HotelDetailsResponse>(h),
                CreatedAt = h.CreatedAt,
                UpdatedAt = h.UpdatedAt,
                Owner = h.Owner,
                NumberOfRooms = h.Rooms.Count
            })
            .Include(h => h.City)
            .Include(h => h.ThumbnailImage)
            .Where(h => h.Id == query.Id);
    }
}