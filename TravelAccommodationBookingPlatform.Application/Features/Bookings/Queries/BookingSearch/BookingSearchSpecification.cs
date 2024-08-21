using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingSearch.DTOs;
using TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingSearch.SpecificationExtensions;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingSearch;

public sealed class BookingSearchSpecification : Specification<Booking, BookingSearchResult>
{
    public BookingSearchSpecification(Guid userId, BookingSearchFilters filters)
    {
        Query.Select(b => new BookingSearchResult
            {
                Id = b.Id,
                Checking = b.Checking,
                Hotel = new()
                {
                    Id = b.Rooms.First().Hotel.Id,
                    Name = b.Rooms.First().Hotel.Name,
                    StarRate = b.Rooms.First().Hotel.StarRate,
                    ThumbnailImage = b.Rooms.First().Hotel.ThumbnailImage,
                    MinimumPrice = new Price { Value = b.Rooms.First().Hotel.Rooms.Min(r => r.Price.Value) },
                    MaximumPrice = new Price { Value = b.Rooms.First().Hotel.Rooms.Max(r => r.Price.Value) }
                }
            })
            .Where(b => b.UserId == userId)
            .ApplyBookingSearchFilters(filters, userId)
            .OrderByDescending(b => b.Checking.CheckInDate)
            .ThenByDescending(b => b.Checking.CheckOutDate)
            .ThenBy(b => b.Id);
    }
}