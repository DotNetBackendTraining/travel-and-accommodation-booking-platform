using TravelAccommodationBookingPlatform.Application.Features.Hotels.Commands.DeleteHotel.Specifications;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Commands.DeleteHotel;

public class DeleteHotelCommandHandler : ICommandHandler<DeleteHotelCommand>
{
    private readonly IRepository<Hotel> _repository;
    private readonly ICudRepository<Hotel> _cudRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteHotelCommandHandler(
        IRepository<Hotel> repository,
        ICudRepository<Hotel> cudRepository,
        IImageRepository imageRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _cudRepository = cudRepository;
        _unitOfWork = unitOfWork;
        _imageRepository = imageRepository;
    }

    public async Task<Result> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
    {
        var hotelSpec = new HotelByIdIncludingImagesSpecification(request.Id);
        var hotel = await _repository.GetAsync(hotelSpec, cancellationToken);
        if (hotel is null)
        {
            return Result.Failure(DomainErrors.Hotel.IdNotFound);
        }

        var hasNoBookingsSpec = new HotelByIdAndHasNoBookingsSpecification(request.Id);
        var hasNoBookings = await _repository.ExistsAsync(hasNoBookingsSpec, cancellationToken);
        if (!hasNoBookings)
        {
            return Result.Failure(DomainErrors.Hotel.CannotDeleteHotelWithBookings);
        }

        _imageRepository.Delete(hotel.ThumbnailImage);
        _imageRepository.DeleteAll(hotel.Images);
        _cudRepository.Delete(hotel);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}