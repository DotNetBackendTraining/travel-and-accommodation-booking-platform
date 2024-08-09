using AutoMapper;
using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Cities.Specifications;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Cities.Commands.PatchCity;

public class PatchCityCommandHandler : ICommandHandler<PatchCityCommand>
{
    private readonly IRepository<City> _repository;
    private readonly ICudRepository<City> _cudRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<PatchCityModel> _validator;

    public PatchCityCommandHandler(
        IRepository<City> repository,
        ICudRepository<City> cudRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<PatchCityModel> validator)
    {
        _repository = repository;
        _cudRepository = cudRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result> Handle(PatchCityCommand request, CancellationToken cancellationToken)
    {
        // City must exist
        var citySpec = new CityByIdSpecification(request.Id);
        var city = await _repository.GetAsync(citySpec, cancellationToken);
        if (city is null)
        {
            return Result.Failure(DomainErrors.City.IdNotFound);
        }

        // Apply patch to model
        var cityModel = _mapper.Map<PatchCityModel>(city);
        request.PatchDocument.ApplyTo(cityModel);

        // Validate changes
        await _validator.ValidateAndThrowAsync(cityModel, cancellationToken);

        // Apply back to entity
        _mapper.Map(cityModel, city);

        // Update and save changes
        _cudRepository.Update(city);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}