using Ardalis.Specification;
using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingDetails.DTOs;
using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingDetails;

public sealed class BookingDetailsSpecification : Specification<Booking, BookingDetailsResponse>
{
    public BookingDetailsSpecification(
        Guid id,
        Guid userId,
        BookingDetailsParameters parameters,
        IMapper mapper)
    {
        Query.Select(b => new BookingDetailsResponse
            {
                Booking = mapper.Map<BookingDetailsResult>(b),
                BookingHasConfirmedPayment = b.Payment != null,
                PriceCalculation = parameters.IncludeCalculatedPrice
                    ? new PriceCalculationResponse
                    {
                        OriginalPrice = new Price { Value = b.Rooms.Sum(r => r.Price.Value) },
                        AppliedDiscount = b.Payment != null
                            ? b.Payment.AppliedDiscount
                            : b.Rooms.First().Hotel.ActiveDiscount
                    }
                    : null,
                Hotel = parameters.IncludeHotelDetails
                    ? mapper.Map<BookingDetailsHotelResult>(b.Rooms.First().Hotel)
                    : null,
                Rooms = parameters.IncludeRoomsList
                    ? b.Rooms
                        .AsQueryable()
                        .Select(r => mapper.Map<BookingDetailsRoomResult>(r))
                        .ToList()
                    : null
            })
            .Where(b =>
                b.Id == id &&
                b.UserId == userId);
    }
}