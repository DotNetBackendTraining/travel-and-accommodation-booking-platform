using AutoMapper;
using FluentValidation;
using MediatR;
using TravelAccommodationBookingPlatform.Application.Cities.Specifications;
using TravelAccommodationBookingPlatform.Application.Hotels.Specifications;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Commands.PatchHotel;

public class PatchHotelCommandHandler : IRequestHandler<PatchHotelCommand, Result>
{
    private readonly IRepository<Hotel> _hotelRepository;
    private readonly IRepository<City> _cityRepository;
    private readonly ICudRepository<Hotel> _cudHotelRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<PatchHotelModel> _validator;

    public PatchHotelCommandHandler(
        IRepository<Hotel> hotelRepository,
        IRepository<City> cityRepository,
        ICudRepository<Hotel> cudHotelRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<PatchHotelModel> validator)
    {
        _hotelRepository = hotelRepository;
        _cityRepository = cityRepository;
        _cudHotelRepository = cudHotelRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result> Handle(PatchHotelCommand request, CancellationToken cancellationToken)
    {
        // Hotel must exist
        var hotelSpec = new HotelByIdSpecification(request.Id);
        var hotel = await _hotelRepository.GetAsync(hotelSpec, cancellationToken);
        if (hotel is null)
        {
            return Result.Failure(DomainErrors.Hotel.IdNotFound);
        }

        // Apply patch to model
        var updateHotelModel = _mapper.Map<PatchHotelModel>(hotel);
        request.PatchDocument.ApplyTo(updateHotelModel);

        // Validate changes
        await _validator.ValidateAndThrowAsync(updateHotelModel, cancellationToken);

        // Apply back to entity
        _mapper.Map(updateHotelModel, hotel);

        // DB Integrity conditions
        // City must exist
        var citySpec = new CityByIdSpecification(hotel.CityId);
        var cityExists = await _cityRepository.ExistsAsync(citySpec, cancellationToken);
        if (!cityExists)
        {
            return Result.Failure(DomainErrors.City.IdNotFound);
        }

        // Update and save changes
        _cudHotelRepository.Update(hotel);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}