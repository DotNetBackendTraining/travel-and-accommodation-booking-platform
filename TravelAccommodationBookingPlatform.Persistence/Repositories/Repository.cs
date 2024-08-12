using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.Interfaces.Specifications;
using TravelAccommodationBookingPlatform.Application.Shared;
using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public Repository(
        AppDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> ExistsAsync(
        Specification<TEntity> specification,
        CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>()
            .WithSpecification(specification)
            .AnyAsync(cancellationToken);
    }

    public async Task<int> CountAsync(
        Specification<TEntity> specification,
        CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>()
            .WithSpecification(specification)
            .CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> ListAsync(
        Specification<TEntity> specification,
        CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>()
            .WithSpecification(specification)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntityDto>> ListAsync<TEntityDto>(
        Specification<TEntity, TEntityDto> specification,
        CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>()
            .WithSpecification(specification)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntityDto>> ListWithProjectionAsync<TEntityDto>(
        Specification<TEntity> specification,
        CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>()
            .WithSpecification(specification)
            .ProjectTo<TEntityDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(Specification<TEntity> specification, CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>()
            .WithSpecification(specification)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntityDto?> GetAsync<TEntityDto>(
        Specification<TEntity, TEntityDto> specification,
        CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>()
            .WithSpecification(specification)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntityDto?> GetWithProjectionAsync<TEntityDto>(
        Specification<TEntity> specification,
        CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>()
            .WithSpecification(specification)
            .ProjectTo<TEntityDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PageResponse<TEntity>> PageAsync(
        Specification<TEntity> specification,
        PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<TEntity>()
            .WithSpecification(specification);

        return await PageResponseAsync(query, paginationParameters, cancellationToken);
    }

    public async Task<PageResponse<TEntityDto>> PageAsync<TEntityDto>(
        Specification<TEntity, TEntityDto> specification,
        PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<TEntity>()
            .WithSpecification(specification);

        return await PageResponseAsync(query, paginationParameters, cancellationToken);
    }

    public async Task<PageResponse<TEntityDto>> PageWithProjectionAsync<TEntityDto>(
        Specification<TEntity> specification,
        PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<TEntity>()
            .WithSpecification(specification)
            .ProjectTo<TEntityDto>(_mapper.ConfigurationProvider);

        return await PageResponseAsync(query, paginationParameters, cancellationToken);
    }

    private static async Task<PageResponse<T>> PageResponseAsync<T>(
        IQueryable<T> query,
        PaginationParameters parameters,
        CancellationToken cancellationToken)
    {
        return new PageResponse<T>
        {
            TotalCount = await query.CountAsync(cancellationToken),
            Items = await query.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync(cancellationToken)
        };
    }

    public async Task<TAggregateDto?> AggregateAsync<TAggregateDto>(
        Specification<TEntity> querySpecification,
        AggregateSpecification<TEntity, TAggregateDto> aggregateSpecification,
        CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>()
            .WithSpecification(querySpecification)
            .GroupBy(e => 1)
            .WithSpecification(aggregateSpecification)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TAggregateDto?> AggregateAsync<TEntityDto, TAggregateDto>(
        Specification<TEntity, TEntityDto> querySpecification,
        AggregateSpecification<TEntityDto, TAggregateDto> aggregateSpecification,
        CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>()
            .WithSpecification(querySpecification)
            .GroupBy(e => 1)
            .WithSpecification(aggregateSpecification)
            .FirstOrDefaultAsync(cancellationToken);
    }
}