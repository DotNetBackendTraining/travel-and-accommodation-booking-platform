using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Cities.Commands.CreateCity;

public class CreateCityCommandHandler : ICommandHandler<CreateCityCommand, CreateCityResponse>
{
    private readonly ICudRepository<City> _cudRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCityCommandHandler(
        ICudRepository<City> cudRepository,
        IImageRepository imageRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _cudRepository = cudRepository;
        _imageRepository = imageRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<CreateCityResponse>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var city = _mapper.Map<City>(request);

        _imageRepository.SaveAndUpdate(request.ThumbnailImage, city, c => c.ThumbnailImage);
        _cudRepository.Add(city);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CreateCityResponse(city.Id);
        return Result.Success(response);
    }
}