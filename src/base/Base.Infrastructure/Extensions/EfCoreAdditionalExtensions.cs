using Ardalis.Specification;
using Base.Shared.ResultUtility;
using Microsoft.EntityFrameworkCore;

namespace Base.Infrastructure.Extensions
{

    public static class EfCoreAdditionalExtensions
    {
        public static IQueryable<TEntity> HandleTracking<TEntity>(this IQueryable<TEntity> query, bool track) where TEntity : class?
        {
            if (!track)
            {
                query = query.AsNoTracking();
            }
            return query;
        }

        public static async Task<PaginateResultDto<TEntity>> Paginate<TEntity>(this IQueryable<TEntity> query, int page, int pageSize)
            where TEntity : class
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = Base.Shared.Constants.Page.PageSize;

            var rowsCount = query.Count();
            var listEntity = (await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync()) ?? new List<TEntity>();
            return new PaginateResultDto<TEntity>(page, pageSize, rowsCount, listEntity);
        }
    }
}