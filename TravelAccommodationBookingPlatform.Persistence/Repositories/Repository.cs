using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
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
}