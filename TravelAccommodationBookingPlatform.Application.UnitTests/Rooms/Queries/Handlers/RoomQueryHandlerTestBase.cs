using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.UnitTests.TestBases;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Rooms.Queries.Handlers;

public abstract class RoomQueryHandlerTestBase<TQueryHandler, TQuery, TResponse>
    : QueryHandlerTestBase<TQueryHandler, TQuery, TResponse, IRepository<Room>, Room>
    where TQueryHandler : IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    protected RoomQueryHandlerTestBase() : base(DomainErrors.Room.IdNotFound)
    {
    }
}