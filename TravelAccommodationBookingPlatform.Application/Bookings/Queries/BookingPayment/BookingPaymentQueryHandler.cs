using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingPayment;

public class BookingPaymentQueryHandler : IQueryHandler<BookingPaymentQuery, BookingPaymentResponse>
{
    private readonly IRepository<Payment> _repository;

    public BookingPaymentQueryHandler(IRepository<Payment> repository)
    {
        _repository = repository;
    }

    public async Task<Result<BookingPaymentResponse>> Handle(
        BookingPaymentQuery request,
        CancellationToken cancellationToken)
    {
        var paymentSpec = new BookingPaymentSpecification(request.BookingId, request.UserId);
        var payment = await _repository.GetAsync(paymentSpec, cancellationToken);
        return payment is null
            ? Result.Failure<BookingPaymentResponse>(DomainErrors.Booking.PaymentNotFound)
            : Result.Success(payment);
    }
}