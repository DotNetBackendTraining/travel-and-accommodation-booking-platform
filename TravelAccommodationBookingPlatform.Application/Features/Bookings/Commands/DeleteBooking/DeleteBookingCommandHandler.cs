using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Features.Bookings.Commands.DeleteBooking;

public class DeleteBookingCommandHandler : ICommandHandler<DeleteBookingCommand>
{
    private readonly IRepository<Booking> _repository;
    private readonly ICudRepository<Booking> _cudRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBookingCommandHandler(
        IRepository<Booking> repository,
        ICudRepository<Booking> cudRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _cudRepository = cudRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        var bookingSpec = new DeleteBookingSpecification(request.UserId, request.BookingId);
        var booking = await _repository.GetAsync(bookingSpec, cancellationToken);
        if (booking is null)
        {
            return Result.Failure(DomainErrors.Booking.IdNotFound);
        }

        if (booking.Payment is not null)
        {
            return Result.Failure(DomainErrors.Booking.CannotDeleteBookingWithPayment);
        }

        _cudRepository.Delete(booking);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}