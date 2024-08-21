using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Features.Rooms.Queries.RoomImages;

public class RoomImagesQueryHandler : IQueryHandler<RoomImagesQuery, RoomImagesResponse>
{
    private readonly IRepository<Room> _repository;

    public RoomImagesQueryHandler(IRepository<Room> repository)
    {
        _repository = repository;
    }

    public async Task<Result<RoomImagesResponse>> Handle(
        RoomImagesQuery request,
        CancellationToken cancellationToken)
    {
        var specification = new RoomImagesSpecification(request);
        var roomImagesResponse = await _repository.GetAsync(specification, cancellationToken);

        return roomImagesResponse is null
            ? Result.Failure<RoomImagesResponse>(DomainErrors.Room.IdNotFound)
            : Result.Success(roomImagesResponse);
    }
}