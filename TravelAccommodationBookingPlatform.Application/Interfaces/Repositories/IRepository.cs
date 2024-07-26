using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Retrieves a list of <typeparamref name="TEntity"/> according to the specification.
    /// </summary>
    /// <param name="specification">Specification that will be applied on the query</param>
    /// <param name="cancellationToken">Request cancellation token</param>
    /// <typeparam name="TEntity">Output of applying the specification on the query</typeparam>
    /// <returns>All outputs that obey the specification</returns>
    Task<IEnumerable<TEntity?>> ListAsync(
        Specification<TEntity> specification,
        CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a list of <typeparamref name="TEntityDto"/> according to the specification.
    /// </summary>
    /// <param name="specification">Specification that will be applied on the query</param>
    /// <param name="cancellationToken">Request cancellation token</param>
    /// <typeparam name="TEntityDto">Output of applying the specification on the query</typeparam>
    /// <returns>All outputs that obey the specification</returns>
    Task<IEnumerable<TEntityDto?>> ListAsync<TEntityDto>(
        Specification<TEntity, TEntityDto> specification,
        CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a list of <typeparamref name="TEntity"/> according to the specification.
    /// Projects each <typeparamref name="TEntity"/> into <typeparamref name="TEntityDto"/> with AutoMapper.
    /// </summary>
    /// <param name="specification">Specification that will be applied on the query</param>
    /// <param name="cancellationToken">Request cancellation token</param>
    /// <typeparam name="TEntityDto">The type that the output of the query will be projected to.
    /// So a map profile from <typeparamref name="TEntity"/> into <typeparamref name="TEntityDto"/> should exist.</typeparam>
    /// <returns>All projected outputs that obey the specification</returns>
    Task<IEnumerable<TEntityDto?>> ListWithProjectionAsync<TEntityDto>(
        Specification<TEntity> specification,
        CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the first <typeparamref name="TEntity"/> according to the specification.
    /// </summary>
    /// <param name="specification">Specification that will be applied on the query.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    /// <typeparam name="TEntity">Output of applying the specification on the query.</typeparam>
    /// <returns>The first output that obeys the specification or default if no match is found.</returns>
    Task<TEntity?> GetAsync(
        Specification<TEntity> specification,
        CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the first <typeparamref name="TEntityDto"/> according to the specification.
    /// </summary>
    /// <param name="specification">Specification that will be applied on the query.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    /// <typeparam name="TEntityDto">Output of applying the specification on the query.</typeparam>
    /// <returns>The first output that obeys the specification or default if no match is found.</returns>
    Task<TEntityDto?> GetAsync<TEntityDto>(
        Specification<TEntity, TEntityDto> specification,
        CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the first <typeparamref name="TEntity"/> according to the specification.
    /// Projects it into <typeparamref name="TEntityDto"/> with AutoMapper.
    /// </summary>
    /// <param name="specification">Specification that will be applied on the query.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    /// <typeparam name="TEntityDto">The type that the output of the query will be projected to.
    /// So a map profile from <typeparamref name="TEntity"/> into <typeparamref name="TEntityDto"/> should exist.</typeparam>
    /// <returns>The first projected output that obeys the specification or default if no match is found.</returns>
    Task<TEntityDto?> GetWithProjectionAsync<TEntityDto>(
        Specification<TEntity> specification,
        CancellationToken cancellationToken);
}