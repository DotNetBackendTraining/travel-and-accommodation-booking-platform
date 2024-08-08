using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;

/// <summary>
/// Provides basic Create, Update, and Delete (CUD) operations for entities within a unit of work transaction.
/// </summary>
public interface ICudRepository<in TEntity> where TEntity : BaseEntity
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}