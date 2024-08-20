using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelDetails.Admin;

public class AdminHotelDetailsQueryHandler : IQueryHandler<AdminHotelDetailsQuery, AdminHotelDetailsResponse>
{
    private readonly IRepository<Hotel> _repository;
    private readonly IMapper _mapper;

    public AdminHotelDetailsQueryHandler(
        IRepository<Hotel> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<AdminHotelDetailsResponse>> Handle(
        AdminHotelDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new AdminHotelDetailsSpecification(request, _mapper);
        var hotelAdminDetailsResponse = await _repository.GetAsync(spec, cancellationToken);

        return hotelAdminDetailsResponse is null
            ? Result.Failure<AdminHotelDetailsResponse>(DomainErrors.Hotel.IdNotFound)
            : Result.Success(hotelAdminDetailsResponse);
    }
}