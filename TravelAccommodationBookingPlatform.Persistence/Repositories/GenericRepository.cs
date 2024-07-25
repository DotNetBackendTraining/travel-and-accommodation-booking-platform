using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Application;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.Shared;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Persistence.Extensions;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GenericRepository(
        AppDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TEntityDto?>> GetBySpecificationAsync<TEntityDto>(
        Specification<TEntity> specification,
        CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>()
            .ApplySpecification(specification)
            .ProjectTo<TEntityDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntityDto?>> GetBySpecificationAsync<TEntityDto>(
        Specification<TEntity, TEntityDto> specification,
        CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>()
            .ApplySpecification(specification)
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntityDto?> GetFirstBySpecificationAsync<TEntityDto>(
        Specification<TEntity> specification,
        CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>()
            .ApplySpecification(specification)
            .ProjectTo<TEntityDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntityDto?> GetFirstBySpecificationAsync<TEntityDto>(
        Specification<TEntity, TEntityDto> specification,
        CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>()
            .ApplySpecification(specification)
            .FirstOrDefaultAsync(cancellationToken);
    }
}