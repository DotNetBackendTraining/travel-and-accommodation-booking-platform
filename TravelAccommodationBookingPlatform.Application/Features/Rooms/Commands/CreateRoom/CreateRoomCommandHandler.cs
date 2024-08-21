using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Specifications;
using TravelAccommodationBookingPlatform.Application.Features.Rooms.Specifications;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Features.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler : ICommandHandler<CreateRoomCommand, CreateRoomResponse>
{
    private readonly IRepository<Hotel> _hotelRepository;
    private readonly IRepository<Room> _roomRepository;
    private readonly ICudRepository<Room> _roomCudRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateRoomCommandHandler(
        IRepository<Hotel> hotelRepository,
        IRepository<Room> roomRepository,
        ICudRepository<Room> roomCudRepository,
        IImageRepository imageRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _roomRepository = roomRepository;
        _roomCudRepository = roomCudRepository;
        _imageRepository = imageRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<CreateRoomResponse>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var hotelSpec = new HotelByIdSpecification(request.HotelId);
        var hotelExists = await _hotelRepository.ExistsAsync(hotelSpec, cancellationToken);
        if (!hotelExists)
        {
            return Result.Failure<CreateRoomResponse>(DomainErrors.Hotel.IdNotFound);
        }

        var roomNumberSpec = new RoomByRoomNumberSpecification(request.HotelId, request.RoomNumber);
        var roomNumberExists = await _roomRepository.ExistsAsync(roomNumberSpec, cancellationToken);
        if (roomNumberExists)
        {
            return Result.Failure<CreateRoomResponse>(DomainErrors.Room.RoomNumberAlreadyExists);
        }

        var room = _mapper.Map<Room>(request);

        _imageRepository.SaveAndSetAll(request.Images, room, r => r.Images);
        _roomCudRepository.Add(room);

        await _unitOfWork.CommitAsync(cancellationToken);

        var response = new CreateRoomResponse(room.Id);
        return Result.Success(response);
    }
}