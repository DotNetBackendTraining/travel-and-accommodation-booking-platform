using MediatR;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;