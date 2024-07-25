using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Application.Shared;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Extensions;

public static class SpecificationEvaluatorExtensions
{
    private static IQueryable<TEntity> ApplySpecificationBase<TEntity>(
        IQueryable<TEntity> queryable,
        Specification<TEntity> specification)
        where TEntity : BaseEntity
    {
        if (specification.Criteria is not null)
        {
            queryable = queryable.Where(specification.Criteria);
        }

        queryable = specification.IncludeExpressions.Aggregate(queryable,
            (current, includeExpression) => current.Include(includeExpression));

        if (specification.OrderByExpression is not null)
        {
            queryable = queryable.OrderBy(specification.OrderByExpression);
        }
        else if (specification.OrderByDescendingExpression is not null)
        {
            queryable = queryable.OrderByDescending(specification.OrderByDescendingExpression);
        }

        if (specification.UseSplitQuery)
        {
            queryable = queryable.AsSplitQuery();
        }

        return queryable;
    }

    public static IQueryable<TEntity> ApplySpecification<TEntity>(
        this IQueryable<TEntity> inputQueryable,
        Specification<TEntity> specification)
        where TEntity : BaseEntity
    {
        return ApplySpecificationBase(inputQueryable, specification);
    }

    public static IQueryable<TOutput> ApplySpecification<TEntity, TOutput>(
        this IQueryable<TEntity> inputQueryable,
        Specification<TEntity, TOutput> specification)
        where TEntity : BaseEntity
    {
        var queryable = ApplySpecificationBase(inputQueryable, specification);

        if (specification.Selector is not null)
        {
            return queryable.Select(specification.Selector);
        }

        throw new InvalidOperationException("Selector must be specified in the specification.");
    }
}