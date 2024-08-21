using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.Rooms.Commands.DeleteRoom.Specifications;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Commands.DeleteRoom;

public class DeleteRoomCommandHandler : ICommandHandler<DeleteRoomCommand>
{
    private readonly IRepository<Room> _repository;
    private readonly ICudRepository<Room> _cudRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoomCommandHandler(
        IRepository<Room> repository,
        ICudRepository<Room> cudRepository,
        IImageRepository imageRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _cudRepository = cudRepository;
        _imageRepository = imageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var roomSpec = new RoomByIdIncludingImagesSpecification(request.Id);
        var room = await _repository.GetAsync(roomSpec, cancellationToken);
        if (room is null)
        {
            return Result.Failure(DomainErrors.Room.IdNotFound);
        }

        var hasNoBookingsSpec = new RoomByIdAndHasNoBookingsSpecification(request.Id);
        var hasNoBookings = await _repository.ExistsAsync(hasNoBookingsSpec, cancellationToken);
        if (!hasNoBookings)
        {
            return Result.Failure(DomainErrors.Room.CannotDeleteRoomWithBookings);
        }

        _imageRepository.DeleteAll(room.Images);
        _cudRepository.Delete(room);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}