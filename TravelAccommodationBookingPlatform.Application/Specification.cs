using System.Linq.Expressions;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application;

public class Specification<TEntity> where TEntity : BaseEntity
{
    public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

    public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }

    public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }

    public bool UseSplitQuery { get; private set; }

    private Specification()
    {
    }

    public class Builder
    {
        private Expression<Func<TEntity, bool>>? _criteria;
        private readonly List<Expression<Func<TEntity, object>>> _includes = [];
        private Expression<Func<TEntity, object>>? _orderBy;
        private Expression<Func<TEntity, object>>? _orderByDescending;
        private bool _useSplitQuery;

        public Builder SetCriteria(Expression<Func<TEntity, bool>> criteria)
        {
            _criteria = criteria;
            return this;
        }

        public Builder AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            _includes.Add(includeExpression);
            return this;
        }

        public Builder SetOrderBy(Expression<Func<TEntity, object>> orderByExpression, bool descending = false)
        {
            if (descending)
            {
                _orderByDescending = orderByExpression;
            }
            else
            {
                _orderBy = orderByExpression;
            }

            return this;
        }

        public Builder UseSplitQuery(bool useSplitQuery = true)
        {
            _useSplitQuery = useSplitQuery;
            return this;
        }

        public Specification<TEntity> Build()
        {
            var specification = new Specification<TEntity>
            {
                Criteria = _criteria,
                OrderByExpression = _orderBy,
                OrderByDescendingExpression = _orderByDescending
            };

            specification.IncludeExpressions.AddRange(_includes);

            return specification;
        }
    }
}