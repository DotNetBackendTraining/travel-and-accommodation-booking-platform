using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Cities.Specifications;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Commands.CreateHotel;

public class CreateHotelCommandHandler : ICommandHandler<CreateHotelCommand, CreateHotelResponse>
{
    private readonly IRepository<City> _cityRepository;
    private readonly ICudRepository<Hotel> _hotelCudRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateHotelCommandHandler(
        IRepository<City> cityRepository,
        ICudRepository<Hotel> hotelCudRepository,
        IImageRepository imageRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _hotelCudRepository = hotelCudRepository;
        _imageRepository = imageRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cityRepository = cityRepository;
    }

    public async Task<Result<CreateHotelResponse>> Handle(
        CreateHotelCommand request,
        CancellationToken cancellationToken)
    {
        var citySpec = new CityByIdSpecification(request.CityId);
        var cityExists = await _cityRepository.ExistsAsync(citySpec, cancellationToken);
        if (!cityExists)
        {
            return Result.Failure<CreateHotelResponse>(DomainErrors.City.IdNotFound);
        }

        var hotel = _mapper.Map<Hotel>(request);

        _imageRepository.SaveAndSet(request.ThumbnailImage, hotel, h => h.ThumbnailImage);
        _imageRepository.SaveAndSetAll(request.Images, hotel, h => h.Images);
        _hotelCudRepository.Add(hotel);

        await _unitOfWork.CommitAsync(cancellationToken);
        return Result.Success(new CreateHotelResponse(hotel.Id));
    }
}