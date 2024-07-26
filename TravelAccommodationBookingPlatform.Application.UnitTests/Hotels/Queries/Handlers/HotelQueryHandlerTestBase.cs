using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.UnitTests.TestBases;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Handlers;

public abstract class HotelQueryHandlerTestBase<TQueryHandler, TQuery, TResponse>
    : QueryHandlerTestBase<TQueryHandler, TQuery, TResponse, IRepository<Hotel>, Hotel>
    where TQueryHandler : IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    protected HotelQueryHandlerTestBase() : base(DomainErrors.Hotel.IdNotFound)
    {
    }
}