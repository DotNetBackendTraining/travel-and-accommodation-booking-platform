using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Queries.RoomDetails;

public class RoomDetailsQueryHandler : IQueryHandler<RoomDetailsQuery, RoomDetailsResponse>
{
    private readonly IRepository<Room> _repository;

    public RoomDetailsQueryHandler(IRepository<Room> repository)
    {
        _repository = repository;
    }

    public async Task<Result<RoomDetailsResponse>> Handle(RoomDetailsQuery request, CancellationToken cancellationToken)
    {
        var specification = new RoomDetailsSpecification(request);
        var roomDetailsResponse = await _repository
            .GetWithProjectionAsync<RoomDetailsResponse>(specification, cancellationToken);

        return roomDetailsResponse is null
            ? Result.Failure<RoomDetailsResponse>(DomainErrors.Room.IdNotFound)
            : Result.Success(roomDetailsResponse);
    }
}