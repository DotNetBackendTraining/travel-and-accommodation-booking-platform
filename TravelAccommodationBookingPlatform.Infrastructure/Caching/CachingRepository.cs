using Ardalis.Specification;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.Interfaces.Specifications;
using TravelAccommodationBookingPlatform.Application.Shared;
using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Infrastructure.Caching;

public class CachingRepository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private const int CachingTimeMinutes = 10;

    private readonly IRepository<TEntity> _decorated;
    private readonly IMemoryCache _cache;
    private readonly ILogger<CachingRepository<TEntity>> _logger;

    public CachingRepository(
        IRepository<TEntity> decorated,
        IMemoryCache cache,
        ILogger<CachingRepository<TEntity>> logger)
    {
        _decorated = decorated;
        _cache = cache;
        _logger = logger;
    }

    private async Task<TValue> GetOrAddCacheAsync<TValue>(
        ISpecification<TEntity> specification,
        string? methodSignature,
        Func<Task<TValue>> fetchData)
    {
        if (!specification.CacheEnabled)
        {
            _logger.LogWarning(
                "Cache disabled for specification: {@Specification}. Fetching data from the repository.",
                specification);

            return await fetchData();
        }

        var cacheKey = $"{specification.CacheKey}-{methodSignature}";

        if (_cache.TryGetValue(cacheKey, out TValue? cachedValue))
        {
            _logger.LogInformation("Cache hit for key: {CacheKey}. Returning cached data.", cacheKey);
            return cachedValue!;
        }

        _logger.LogWarning("Cache miss for key: {CacheKey}. Fetching data from the repository.", cacheKey);
        var data = await fetchData();
        _cache.Set(cacheKey, data, TimeSpan.FromMinutes(CachingTimeMinutes));

        return data;
    }

    public Task<bool> ExistsAsync(
        Specification<TEntity> specification,
        CancellationToken cancellationToken)
    {
        return GetOrAddCacheAsync(
            specification,
            nameof(ExistsAsync),
            () => _decorated.ExistsAsync(specification, cancellationToken));
    }

    public Task<int> CountAsync(
        Specification<TEntity> specification,
        CancellationToken cancellationToken)
    {
        return GetOrAddCacheAsync(
            specification,
            nameof(CountAsync),
            () => _decorated.CountAsync(specification, cancellationToken));
    }

    public Task<IEnumerable<TEntity>> ListAsync(
        Specification<TEntity> specification,
        CancellationToken cancellationToken)
    {
        return GetOrAddCacheAsync(
            specification,
            nameof(ListAsync),
            () => _decorated.ListAsync(specification, cancellationToken));
    }

    public Task<IEnumerable<TEntityDto>> ListAsync<TEntityDto>(
        Specification<TEntity, TEntityDto> specification,
        CancellationToken cancellationToken)
    {
        return GetOrAddCacheAsync(
            specification,
            $"{nameof(ListAsync)}-{typeof(TEntityDto).Name}",
            () => _decorated.ListAsync(specification, cancellationToken));
    }

    public Task<IEnumerable<TEntityDto>> ListWithProjectionAsync<TEntityDto>(
        Specification<TEntity> specification,
        CancellationToken cancellationToken)
    {
        return GetOrAddCacheAsync(
            specification,
            $"{nameof(ListWithProjectionAsync)}-{typeof(TEntityDto).Name}",
            () => _decorated.ListWithProjectionAsync<TEntityDto>(specification, cancellationToken));
    }

    public Task<TEntity?> GetAsync(
        Specification<TEntity> specification,
        CancellationToken cancellationToken)
    {
        return GetOrAddCacheAsync(
            specification,
            nameof(GetAsync),
            () => _decorated.GetAsync(specification, cancellationToken));
    }

    public Task<TEntityDto?> GetAsync<TEntityDto>(
        Specification<TEntity, TEntityDto> specification,
        CancellationToken cancellationToken)
    {
        return GetOrAddCacheAsync(
            specification,
            $"{nameof(GetAsync)}-{typeof(TEntityDto).Name}",
            () => _decorated.GetAsync(specification, cancellationToken));
    }

    public Task<TEntityDto?> GetWithProjectionAsync<TEntityDto>(
        Specification<TEntity> specification,
        CancellationToken cancellationToken)
    {
        return GetOrAddCacheAsync(
            specification,
            $"{nameof(GetWithProjectionAsync)}-{typeof(TEntityDto).Name}",
            () => _decorated.GetWithProjectionAsync<TEntityDto>(specification, cancellationToken));
    }

    public Task<PageResponse<TEntity>> PageAsync(
        Specification<TEntity> specification,
        PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return GetOrAddCacheAsync(
            specification,
            $"{nameof(PageAsync)}-{paginationParameters.PageNumber}-{paginationParameters.PageSize}",
            () => _decorated.PageAsync(specification, paginationParameters, cancellationToken));
    }

    public Task<PageResponse<TEntityDto>> PageAsync<TEntityDto>(
        Specification<TEntity, TEntityDto> specification,
        PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return GetOrAddCacheAsync(
            specification,
            $"{nameof(PageAsync)}-{typeof(TEntityDto).Name}-{paginationParameters.PageNumber}-{paginationParameters.PageSize}",
            () => _decorated.PageAsync(specification, paginationParameters, cancellationToken));
    }

    public Task<PageResponse<TEntityDto>> PageWithProjectionAsync<TEntityDto>(
        Specification<TEntity> specification,
        PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return GetOrAddCacheAsync(
            specification,
            $"{nameof(PageWithProjectionAsync)}-{typeof(TEntityDto).Name}-{paginationParameters.PageNumber}-{paginationParameters.PageSize}",
            () => _decorated.PageWithProjectionAsync<TEntityDto>(
                specification, paginationParameters, cancellationToken));
    }

    public async Task<TAggregateDto?> AggregateAsync<TAggregateDto>(
        Specification<TEntity> querySpecification,
        AggregateSpecification<TEntity, TAggregateDto> aggregateSpecification,
        CancellationToken cancellationToken)
    {
        return await _decorated.AggregateAsync(querySpecification, aggregateSpecification, cancellationToken);
    }

    public async Task<TAggregateDto?> AggregateAsync<TEntityDto, TAggregateDto>(
        Specification<TEntity, TEntityDto> querySpecification,
        AggregateSpecification<TEntityDto, TAggregateDto> aggregateSpecification,
        CancellationToken cancellationToken)
    {
        return await _decorated.AggregateAsync(querySpecification, aggregateSpecification, cancellationToken);
    }
}