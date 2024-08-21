using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Bookings.Commands.CreateBooking.Specifications;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Commands.CreateBooking;

public class CreateBookingCommandHandler : ICommandHandler<CreateBookingCommand, CreateBookingResponse>
{
    private readonly IRepository<Room> _roomRepository;
    private readonly ICudRepository<Booking> _bookingCudRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBookingCommandHandler(
        IRepository<Room> roomRepository,
        ICudRepository<Booking> bookingCudRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _roomRepository = roomRepository;
        _bookingCudRepository = bookingCudRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<CreateBookingResponse>> Handle(
        CreateBookingCommand request,
        CancellationToken cancellationToken)
    {
        var roomsSpec = new CreateBookingRoomsSpecification(request.RoomIds);
        var roomsCount = await _roomRepository.CountAsync(roomsSpec, cancellationToken);
        if (roomsCount != request.RoomIds.Count)
        {
            // We know it's a unique list from the validator, so at least one roomId was not found
            return Result.Failure<CreateBookingResponse>(DomainErrors.Room.IdNotFound);
        }

        var validitySpec = new CreateBookingValiditySpecification(request.NumberOfGuests);
        var bookingIsValid = await _roomRepository.AggregateAsync(roomsSpec, validitySpec, cancellationToken);
        if (!bookingIsValid)
        {
            return Result.Failure<CreateBookingResponse>(DomainErrors.Booking.InvalidBookingOperation);
        }

        var availabilitySpec = new CreateBookingAvailabilitySpecification(request.Checking);
        var bookingIsAvailable = await _roomRepository.AggregateAsync(roomsSpec, availabilitySpec, cancellationToken);
        if (!bookingIsAvailable)
        {
            return Result.Failure<CreateBookingResponse>(DomainErrors.Booking.RoomsNotAvailable);
        }

        var booking = _mapper.Map<Booking>(request);
        booking.Rooms = (await _roomRepository.ListAsync(roomsSpec, cancellationToken)).ToList();

        _bookingCudRepository.Add(booking);
        await _unitOfWork.CommitAsync(cancellationToken);

        var response = new CreateBookingResponse(booking.Id);
        return Result.Success(response);
    }
}