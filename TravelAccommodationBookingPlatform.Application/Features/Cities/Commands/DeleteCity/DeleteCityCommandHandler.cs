using TravelAccommodationBookingPlatform.Application.Features.Cities.Commands.DeleteCity.Specifications;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Features.Cities.Commands.DeleteCity;

public class DeleteCityCommandHandler : ICommandHandler<DeleteCityCommand>
{
    private readonly IRepository<City> _repository;
    private readonly ICudRepository<City> _cudRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCityCommandHandler(
        IRepository<City> repository,
        ICudRepository<City> cudRepository,
        IImageRepository imageRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _cudRepository = cudRepository;
        _imageRepository = imageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        var citySpec = new CityByIdIncludingImagesSpecification(request.Id);
        var city = await _repository.GetAsync(citySpec, cancellationToken);
        if (city is null)
        {
            return Result.Failure(DomainErrors.City.IdNotFound);
        }

        var hasNoHotelsSpec = new CityByIdAndHasNotHotelsSpecification(request.Id);
        var hasNoHotels = await _repository.ExistsAsync(hasNoHotelsSpec, cancellationToken);
        if (!hasNoHotels)
        {
            return Result.Failure(DomainErrors.City.CannotDeleteCityWithHotels);
        }

        _imageRepository.Delete(city.ThumbnailImage);
        _cudRepository.Delete(city);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}