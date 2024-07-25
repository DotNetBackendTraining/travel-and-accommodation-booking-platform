using System.Linq.Expressions;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Shared;

public class Specification<TEntity> where TEntity : BaseEntity
{
    public Expression<Func<TEntity, bool>>? Criteria { get; protected set; }
    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();
    public Expression<Func<TEntity, object>>? OrderByExpression { get; protected set; }
    public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; protected set; }
    public bool UseSplitQuery { get; protected set; }

    protected Specification()
    {
    }

    public class Builder
    {
        private Expression<Func<TEntity, bool>>? _criteria;
        private readonly List<Expression<Func<TEntity, object>>> _includes = new();
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
            return new Specification<TEntity>
            {
                Criteria = _criteria,
                OrderByExpression = _orderBy,
                OrderByDescendingExpression = _orderByDescending,
                UseSplitQuery = _useSplitQuery
            }.WithIncludes(_includes);
        }
    }

    private Specification<TEntity> WithIncludes(List<Expression<Func<TEntity, object>>> includes)
    {
        IncludeExpressions.AddRange(includes);
        return this;
    }
}

public class Specification<TEntity, TOutput> : Specification<TEntity> where TEntity : BaseEntity
{
    public Expression<Func<TEntity, TOutput>>? Selector { get; private set; }

    private Specification()
    {
    }

    public new class Builder
    {
        private Expression<Func<TEntity, bool>>? _criteria;
        private readonly List<Expression<Func<TEntity, object>>> _includes = new();
        private Expression<Func<TEntity, object>>? _orderBy;
        private Expression<Func<TEntity, object>>? _orderByDescending;
        private Expression<Func<TEntity, TOutput>>? _selector;
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

        public Builder SetSelector(Expression<Func<TEntity, TOutput>> selector)
        {
            _selector = selector;
            return this;
        }

        public Builder UseSplitQuery(bool useSplitQuery = true)
        {
            _useSplitQuery = useSplitQuery;
            return this;
        }

        public Specification<TEntity, TOutput> Build()
        {
            if (_selector is null)
            {
                throw new InvalidOperationException("Selector must be specified in the specification.");
            }

            return new Specification<TEntity, TOutput>
            {
                Criteria = _criteria,
                OrderByExpression = _orderBy,
                OrderByDescendingExpression = _orderByDescending,
                Selector = _selector,
                UseSplitQuery = _useSplitQuery
            }.WithIncludes(_includes);
        }
    }

    private Specification<TEntity, TOutput> WithIncludes(List<Expression<Func<TEntity, object>>> includes)
    {
        IncludeExpressions.AddRange(includes);
        return this;
    }
}