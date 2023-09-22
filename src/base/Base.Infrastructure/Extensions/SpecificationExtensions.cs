using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

namespace Base.Infrastructure.Extensions
{

    public static class SpecificationExtensions
    {

        public static IQueryable<TEntity> ApplySpecification<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification) where TEntity : class
        {
            return SpecificationEvaluator.Default.GetQuery(query, specification);
        }
    }
}