using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelImages;

public class HotelImagesQueryHandler : IQueryHandler<HotelImagesQuery, HotelImagesResponse>
{
    private readonly IRepository<Hotel> _repository;

    public HotelImagesQueryHandler(IRepository<Hotel> repository)
    {
        _repository = repository;
    }

    public async Task<Result<HotelImagesResponse>> Handle(
        HotelImagesQuery request,
        CancellationToken cancellationToken)
    {
        var specification = new HotelImagesSpecification(request);
        var hotelImagesResponse = await _repository.GetAsync(specification, cancellationToken);

        return hotelImagesResponse is null
            ? Result.Failure<HotelImagesResponse>(DomainErrors.Hotel.IdNotFound)
            : Result.Success(hotelImagesResponse);
    }
}