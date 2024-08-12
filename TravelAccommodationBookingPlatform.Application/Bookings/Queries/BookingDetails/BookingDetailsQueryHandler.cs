using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingDetails;

public class BookingDetailsQueryHandler : IQueryHandler<BookingDetailsQuery, BookingDetailsResponse>
{
    private readonly IRepository<Booking> _repository;
    private readonly IMapper _mapper;

    public BookingDetailsQueryHandler(
        IRepository<Booking> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<BookingDetailsResponse>> Handle(
        BookingDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var bookingSpec = new BookingDetailsSpecification(request.Id, request.UserId, request.Parameters, _mapper);
        var booking = await _repository.GetAsync(bookingSpec, cancellationToken);
        return booking is null
            ? Result.Failure<BookingDetailsResponse>(DomainErrors.Booking.IdNotFound)
            : Result.Success(booking);
    }
}