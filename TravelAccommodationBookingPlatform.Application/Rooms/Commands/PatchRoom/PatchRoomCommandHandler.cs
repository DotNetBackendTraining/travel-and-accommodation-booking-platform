using AutoMapper;
using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Hotels.Specifications;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.Rooms.Specifications;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Commands.PatchRoom;

public class PatchRoomCommandHandler : ICommandHandler<PatchRoomCommand>
{
    private readonly IRepository<Room> _roomRepository;
    private readonly IRepository<Hotel> _hotelRepository;
    private readonly ICudRepository<Room> _roomCudRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<PatchRoomModel> _validator;

    public PatchRoomCommandHandler(
        IRepository<Room> roomRepository,
        IRepository<Hotel> hotelRepository,
        ICudRepository<Room> roomCudRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<PatchRoomModel> validator)
    {
        _roomRepository = roomRepository;
        _hotelRepository = hotelRepository;
        _roomCudRepository = roomCudRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result> Handle(PatchRoomCommand request, CancellationToken cancellationToken)
    {
        // Room must exist
        var roomSpec = new RoomByIdSpecification(request.Id);
        var room = await _roomRepository.GetAsync(roomSpec, cancellationToken);
        if (room is null)
        {
            return Result.Failure(DomainErrors.Room.IdNotFound);
        }

        // Apply patch to model
        var roomModel = _mapper.Map<PatchRoomModel>(room);
        request.PatchDocument.ApplyTo(roomModel);

        // Validate changes
        await _validator.ValidateAndThrowAsync(roomModel, cancellationToken);

        // Apply back to entity
        _mapper.Map(roomModel, room);

        // DB Integrity conditions
        // Hotel must exist
        var hotelSpec = new HotelByIdSpecification(room.HotelId);
        var hotelExists = await _hotelRepository.ExistsAsync(hotelSpec, cancellationToken);
        if (!hotelExists)
        {
            return Result.Failure(DomainErrors.Hotel.IdNotFound);
        }

        // Update and save changes
        _roomCudRepository.Update(room);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}