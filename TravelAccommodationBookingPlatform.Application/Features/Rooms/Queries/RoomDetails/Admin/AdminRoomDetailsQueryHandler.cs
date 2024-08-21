using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Features.Rooms.Queries.RoomDetails.Admin;

public class AdminRoomDetailsQueryHandler : IQueryHandler<AdminRoomDetailsQuery, AdminRoomDetailsResponse>
{
    private readonly IRepository<Room> _repository;
    private readonly IMapper _mapper;

    public AdminRoomDetailsQueryHandler(IRepository<Room> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<AdminRoomDetailsResponse>> Handle(
        AdminRoomDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new AdminRoomDetailsSpecification(request.Id, _mapper);
        var adminRoomDetailsResponse = await _repository.GetAsync(spec, cancellationToken);

        return adminRoomDetailsResponse is null
            ? Result.Failure<AdminRoomDetailsResponse>(DomainErrors.Room.IdNotFound)
            : Result.Success(adminRoomDetailsResponse);
    }
}