using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    /// <param name="specification">Specification that will be applied on the query</param>
    /// <param name="cancellationToken">Request cancellation token</param>
    /// <typeparam name="TEntityDto">The type that the output of the query will be projected to.
    /// So a mapper profile for it should exist somewhere</typeparam>
    /// <returns>All projected outputs that obey the specification</returns>
    Task<IEnumerable<TEntityDto?>> GetBySpecificationAsync<TEntityDto>(
        Specification<TEntity> specification,
        CancellationToken cancellationToken);

    /// <param name="specification">Specification that will be applied on the query</param>
    /// <param name="cancellationToken">Request cancellation token</param>
    /// <typeparam name="TEntityDto">Output of applying the specification on the query</typeparam>
    /// <returns>All outputs that obey the specification</returns>
    Task<IEnumerable<TEntityDto?>> GetBySpecificationAsync<TEntityDto>(
        Specification<TEntity, TEntityDto> specification,
        CancellationToken cancellationToken);

    /// <param name="specification">Specification that will be applied on the query.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    /// <typeparam name="TEntityDto">The type that the output of the query will be projected to.
    /// So a mapper profile for it should exist somewhere.</typeparam>
    /// <returns>The first projected output that obeys the specification or default if no match is found.</returns>
    Task<TEntityDto?> GetFirstBySpecificationAsync<TEntityDto>(
        Specification<TEntity> specification,
        CancellationToken cancellationToken);

    /// <param name="specification">Specification that will be applied on the query.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    /// <typeparam name="TEntityDto">Output of applying the specification on the query.</typeparam>
    /// <returns>The first output that obeys the specification or default if no match is found.</returns>
    Task<TEntityDto?> GetFirstBySpecificationAsync<TEntityDto>(
        Specification<TEntity, TEntityDto> specification,
        CancellationToken cancellationToken);
}