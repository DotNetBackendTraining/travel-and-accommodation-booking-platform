using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.FeaturedHotels;

public class FeaturedHotelsQueryHandler : IQueryHandler<FeaturedHotelsQuery, FeaturedHotelsResponse>
{
    private readonly IRepository<Hotel> _repository;
    private readonly IMapper _mapper;

    public FeaturedHotelsQueryHandler(
        IRepository<Hotel> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<FeaturedHotelsResponse>> Handle(
        FeaturedHotelsQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new FeaturedHotelsSpecification(_mapper);
        var page = await _repository.PageAsync(spec, request.PaginationParameters, cancellationToken);
        return Result.Success(new FeaturedHotelsResponse { Results = page });
    }
}