using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.TestBases;

public abstract class QueryHandlerTestBase<TQueryHandler, TQuery, TResponse, TRepository, TEntity>
    where TQueryHandler : IQueryHandler<TQuery, TResponse>
    where TRepository : class, IRepository<TEntity>
    where TQuery : IQuery<TResponse>
    where TEntity : BaseEntity
{
    protected abstract TQueryHandler CreateHandler(TRepository repository);
    protected abstract Task SetupRepositoryForFailure(Mock<TRepository> mockRepository);
    protected abstract Task SetupRepositoryForSuccess(Mock<TRepository> mockRepository, TResponse response);

    private readonly Error _domainEntityNotFoundError;

    protected QueryHandlerTestBase(Error domainEntityNotFoundError)
    {
        _domainEntityNotFoundError = domainEntityNotFoundError;
    }

    [Theory, AutoMoqData]
    public async Task Handle_ReturnsFailure_WhenEntityNotFound(
        [Frozen] Mock<TRepository> mockRepository,
        TQuery query)
    {
        await SetupRepositoryForFailure(mockRepository);
        var handler = CreateHandler(mockRepository.Object);

        var result = await handler.Handle(query, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(_domainEntityNotFoundError);
    }

    [Theory, AutoMoqData]
    public async Task Handle_ReturnsSuccess_WithEntityDetails_WhenEntityIsFound(
        [Frozen] Mock<TRepository> mockRepository,
        TResponse response,
        TQuery query)
    {
        await SetupRepositoryForSuccess(mockRepository, response);
        var handler = CreateHandler(mockRepository.Object);

        var result = await handler.Handle(query, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(response);
    }
}