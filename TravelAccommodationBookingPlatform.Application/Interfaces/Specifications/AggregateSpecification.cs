using Ardalis.Specification;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Specifications;

/// <summary>
/// A specification that defines how to aggregate entities into a specified result type.
/// </summary>
/// <typeparam name="TEntity">The type of the entities to be aggregated.</typeparam>
/// <typeparam name="TAggregateDto">The type that the result of the aggregation will be projected to.</typeparam>
public abstract class AggregateSpecification<TEntity, TAggregateDto>
    : Specification<IGrouping<int, TEntity>, TAggregateDto>;