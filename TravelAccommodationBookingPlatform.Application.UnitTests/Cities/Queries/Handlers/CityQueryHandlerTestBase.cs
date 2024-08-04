using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.UnitTests.TestBases;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Cities.Queries.Handlers;

public abstract class CityQueryHandlerTestBase<TQueryHandler, TQuery, TResponse>
    : QueryHandlerTestBase<TQueryHandler, TQuery, TResponse, IRepository<City>, City>
    where TQueryHandler : IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    protected CityQueryHandlerTestBase() : base(DomainErrors.City.IdNotFound)
    {
    }
}