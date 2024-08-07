using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Cities.Specifications;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Commands.CreateHotel;

public class CreateHotelCommandHandler : ICommandHandler<CreateHotelCommand, CreateHotelResponse>
{
    private readonly IRepository<City> _cityRepository;
    private readonly ICudRepository<Hotel> _hotelCudRepository;
    private readonly IImageStorageService _imageStorageService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateHotelCommandHandler(
        IRepository<City> cityRepository,
        ICudRepository<Hotel> hotelCudRepository,
        IImageStorageService imageStorageService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _hotelCudRepository = hotelCudRepository;
        _imageStorageService = imageStorageService;
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

        var allImages = request.Images.Append(request.ThumbnailImage).ToList();
        var imageSaveResult = await _imageStorageService.SaveAllAsync(allImages);

        if (imageSaveResult.IsFailure)
        {
            return Result.Failure<CreateHotelResponse>(imageSaveResult.Error);
        }

        var imageUrls = imageSaveResult.Value.Take(request.Images.Count()).ToList();
        var thumbnailImageUrl = imageSaveResult.Value.Last();

        var hotel = _mapper.Map<Hotel>(request);
        hotel.ThumbnailImage = new Image { Url = thumbnailImageUrl };
        hotel.Images = imageUrls.Select(url => new Image { Url = url }).ToList();

        try
        {
            _hotelCudRepository.Add(hotel);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(new CreateHotelResponse(hotel.Id));
        }
        catch (Exception)
        {
            foreach (var url in imageSaveResult.Value)
            {
                await _imageStorageService.DeleteAsync(url);
            }

            throw;
        }
    }
}