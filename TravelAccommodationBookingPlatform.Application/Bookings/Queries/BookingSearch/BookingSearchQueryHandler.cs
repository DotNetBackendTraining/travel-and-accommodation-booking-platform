using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingSearch;

public class BookingSearchQueryHandler : IQueryHandler<BookingSearchQuery, BookingSearchResponse>
{
    private readonly IRepository<Booking> _repository;

    public BookingSearchQueryHandler(IRepository<Booking> repository)
    {
        _repository = repository;
    }

    public async Task<Result<BookingSearchResponse>> Handle(
        BookingSearchQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new BookingSearchSpecification(request.UserId, request.Filters);
        var page = await _repository.PageAsync(spec, request.PaginationParameters, cancellationToken);
        return Result.Success(new BookingSearchResponse { Results = page });
    }
}