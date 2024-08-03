using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications.Extensions;

public static class HotelSearchSpecificationExtensions
{
    public static ISpecificationBuilder<Hotel> ApplyHotelSearchFilters(
        this ISpecificationBuilder<Hotel> query,
        HotelSearchQuery.HotelSearchFilters filters)
    {
        return query.ApplySearchTermFilter(filters.SearchTerm)
            .ApplyGeneralFilters(filters.General)
            .ApplyAdvancedFilters(filters.Advanced);
    }

    public static ISpecificationBuilder<Hotel> HasAvailableRoom(
        this ISpecificationBuilder<Hotel> query,
        Checking checking)
    {
        return query.Where(h => h.Rooms.Any(r =>
            !r.Bookings.Any(b =>
                b.Checking.CheckInDate <= checking.CheckOutDate &&
                b.Checking.CheckOutDate >= checking.CheckInDate)));
    }

    public static ISpecificationBuilder<Hotel> ApplySearchTermFilter(
        this ISpecificationBuilder<Hotel> query,
        string searchTerm)
    {
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query.Where(h => h.Name.Contains(searchTerm) || h.City.Name.Contains(searchTerm));
        }

        return query;
    }

    public static ISpecificationBuilder<Hotel> ApplyGeneralFilters(
        this ISpecificationBuilder<Hotel> query,
        HotelSearchQuery.GeneralFilters filters)
    {
        return query
            .HasAvailableRoom(filters.Checking)
            .Where(h => h.Rooms.Count >= filters.Rooms)
            .Where(h => h.Rooms.Sum(r => r.MaxNumberOfGuests.Adults) >= filters.NumberOfGuests.Adults)
            .Where(h => h.Rooms.Sum(r => r.MaxNumberOfGuests.Children) >= filters.NumberOfGuests.Children);
    }

    public static ISpecificationBuilder<Hotel> ApplyAdvancedFilters(
        this ISpecificationBuilder<Hotel> query,
        HotelSearchQuery.AdvancedFilters advancedFilters)
    {
        if (advancedFilters.MinPrice is not null)
        {
            query.Where(h => h.Rooms.Any(r => r.Price.Value >= advancedFilters.MinPrice.Value));
        }

        if (advancedFilters.MaxPrice is not null)
        {
            query.Where(h => h.Rooms.Any(r => r.Price.Value <= advancedFilters.MaxPrice.Value));
        }

        if (advancedFilters.AllowedStarRatings is not null)
        {
            query.Where(h => advancedFilters.AllowedStarRatings.Contains(h.StarRate));
        }

        if (advancedFilters.RequiredAmenities is not null)
        {
            query.Where(h => advancedFilters.RequiredAmenities.All(a => h.Amenities.Contains(a)));
        }

        if (advancedFilters.RequiredRoomTypes is not null)
        {
            query.Where(h => advancedFilters.RequiredRoomTypes.Any(rt => h.Rooms.Any(r => r.RoomType == rt)));
        }

        return query;
    }
}