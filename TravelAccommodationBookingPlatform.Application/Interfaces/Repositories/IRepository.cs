using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Applies specification to get list of <typeparamref name="TEntity"/>.
    /// And maps each <typeparamref name="TEntity"/> into <typeparamref name="TEntityDto"/> object.
    /// </summary>
    /// <param name="specification">Specification that will be applied on the query</param>
    /// <param name="cancellationToken">Request cancellation token</param>
    /// <typeparam name="TEntityDto">The type that the output of the query will be projected to.
    /// So a map profile from <typeparamref name="TEntity"/> into <typeparamref name="TEntityDto"/> should exist.</typeparam>
    /// <returns>All projected outputs that obey the specification</returns>
    Task<IEnumerable<TEntityDto?>> ListBySpecAsync<TEntityDto>(
        Specification<TEntity> specification,
        CancellationToken cancellationToken);

    /// <summary>
    /// Applies specification to get list of <typeparamref name="TEntityDto"/>.
    /// </summary>
    /// <param name="specification">Specification that will be applied on the query</param>
    /// <param name="cancellationToken">Request cancellation token</param>
    /// <typeparam name="TEntityDto">Output of applying the specification on the query</typeparam>
    /// <returns>All outputs that obey the specification</returns>
    Task<IEnumerable<TEntityDto?>> ListBySpecAsync<TEntityDto>(
        Specification<TEntity, TEntityDto> specification,
        CancellationToken cancellationToken);

    /// <summary>
    /// Applies specification to get list of <typeparamref name="TEntity"/>.
    /// And maps each <typeparamref name="TEntity"/> into <typeparamref name="TEntityDto"/> object.
    /// </summary>
    /// <param name="specification">Specification that will be applied on the query.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    /// <typeparam name="TEntityDto">The type that the output of the query will be projected to.
    /// So a map profile from <typeparamref name="TEntity"/> into <typeparamref name="TEntityDto"/> should exist.</typeparam>
    /// <returns>The first projected output that obeys the specification or default if no match is found.</returns>
    Task<TEntityDto?> GetBySpecAsync<TEntityDto>(
        Specification<TEntity> specification,
        CancellationToken cancellationToken);

    /// <summary>
    /// Applies specification to get list of <typeparamref name="TEntityDto"/>.
    /// </summary>
    /// <param name="specification">Specification that will be applied on the query.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    /// <typeparam name="TEntityDto">Output of applying the specification on the query.</typeparam>
    /// <returns>The first output that obeys the specification or default if no match is found.</returns>
    Task<TEntityDto?> GetBySpecAsync<TEntityDto>(
        Specification<TEntity, TEntityDto> specification,
        CancellationToken cancellationToken);
}