using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelRooms;

public class HotelRoomsQueryHandler : IQueryHandler<HotelRoomsQuery, HotelRoomsResponse>
{
    private readonly IRepository<Hotel> _repository;
    private readonly IMapper _mapper;

    public HotelRoomsQueryHandler(
        IRepository<Hotel> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<HotelRoomsResponse>> Handle(
        HotelRoomsQuery request,
        CancellationToken cancellationToken)
    {
        var specification = new HotelRoomsSpecification(request, _mapper);
        var hotelRoomsResponse = await _repository.GetAsync(specification, cancellationToken);

        return hotelRoomsResponse is null
            ? Result.Failure<HotelRoomsResponse>(DomainErrors.Hotel.IdNotFound)
            : Result.Success(hotelRoomsResponse);
    }
}