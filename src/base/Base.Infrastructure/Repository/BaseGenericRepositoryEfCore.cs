using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Ardalis.Specification;

using Base.Infrastructure.Extensions;
using Base.Shared.ResultUtility;
using Base.Shared;
using Base.Core.Repository;
using Base.Core.BaseEntities;
using Microsoft.Data.SqlClient;

namespace Base.Infrastructure.Repository
{

    public class BaseGenericRepository<Tkey, TEntity> : IBaseGenericRepository<Tkey, TEntity>
        where TEntity : BaseEntity<Tkey>
    {
        private readonly DbContext _contex;
        private readonly DbSet<TEntity> DbSetEntity;

        public BaseGenericRepository(DbContext contex)
        {
            _contex = contex;
            DbSetEntity = _contex.Set<TEntity>();
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> filter, CancellationToken token = default)
        {
            return await DbSetEntity.AnyAsync(filter);
        }

        public async Task<TEntity> Get(Tkey key, bool track = false, CancellationToken token = default)
        {
            return await DbSetEntity.FindAsync(key);
        }

        public async Task<TEntityDto> Get<TEntityDto>(Tkey key, bool track = false, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter, bool track = false,
            CancellationToken token = default)
        {
            return await DbSetEntity.Where(filter).HandleTracking(track).FirstOrDefaultAsync();
        }

        public Task<TEntityDto> Get<TEntityDto>(Expression<Func<TEntity, bool>> filter, bool track = false, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This Do Not Stop SqlInjection  , Carefull Using It , Parameter Must Send With SqlParameters
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <param name="track"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAll(string query, SqlParameter[] parameters, bool track = false,
            CancellationToken token = default)
        {
            List<TEntity> result;
            if (!query.Contains("@"))
            {

                result = await DbSetEntity.FromSqlRaw(query).HandleTracking(track).ToListAsync();
            }
            else
            {
                result = await DbSetEntity.FromSqlRaw(query, parameters.ToArray<object>()).HandleTracking(track).ToListAsync();
            }

            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter, bool track = false,
            CancellationToken token = default)
        {
            var query = DbSetEntity.Where(filter);
            return await query!.HandleTracking(track)!.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity
            , TEntity>> select, bool track = false, CancellationToken token = default)
        {
            var query = DbSetEntity.Where(filter).Select(select);
            return await query.HandleTracking(track).ToListAsync();
        }

        public Task<IEnumerable<TEntityDto>> GetAll<TEntityDto>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> select, bool track = false, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAll(ISpecification<TEntity> filter, bool track,
            CancellationToken token = default)
        {
            return await DbSetEntity.ApplySpecification(filter).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll(ISpecification<TEntity> filter, ISpecification<TEntity> select,
            bool track = false, CancellationToken token = default)
        {
            return await DbSetEntity.ApplySpecification(filter)
                .ApplySpecification(select).HandleTracking(track).ToListAsync();
        }

        public async Task<PaginateResultDto<TEntity>> PaginateGetAll(Expression<Func<TEntity, bool>> filter, int page = 1,
            int pageSize = Base.Shared.Constants.Page.PageSize, bool track = false,
            CancellationToken token = default)
        {
            var result = await DbSetEntity.Where(filter).HandleTracking(track).Paginate(page, pageSize);
            return result;
        }

        public async Task<PaginateResultDto<TEntity>> PaginateGetAll(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TEntity>> select, int page = 1, int pageSize = Constants.Page.PageSize,
            bool track = false, CancellationToken token = default)
        {
            var result = await DbSetEntity.Where(filter).Select(select).HandleTracking(track).Paginate(page, pageSize);
            return result;
        }

        public async Task<PaginateResultDto<TEntity>> PaginateGetAll(ISpecification<TEntity> filter,
            ISpecification<TEntity> select, int page = 1, int pageSize = Constants.Page.PageSize,
            bool track = false, CancellationToken token = default)
        {
            var result = await DbSetEntity.ApplySpecification(filter).ApplySpecification(select).HandleTracking(track)
                .Paginate(page, pageSize);
            return result;
        }


        public virtual async Task Create(TEntity entity, CancellationToken token = default)
        {
            await DbSetEntity.AddAsync(entity);
        }

        public virtual async Task Update(TEntity entity, CancellationToken token = default)
        {
            var entityToUpdate = await Get(entity.Id, true);
            entityToUpdate.HandleNotFindEntityInRepositoryException();
            if (_contex.Entry(entity).State == EntityState.Detached || _contex.Entry(entity).State == EntityState.Unchanged)
                _contex.Entry(entity).State = EntityState.Modified;
            DbSetEntity.Update(entity);
        }

        public virtual async Task Remove(TEntity entity, CancellationToken token = default)
        {
            var entityToRemove = await Get(entity.Id, true);
            entityToRemove.HandleNotFindEntityInRepositoryException();
            entityToRemove!.Remove();
            DbSetEntity.Update(entityToRemove);
        }

        public virtual async Task UnRemove(TEntity entity, CancellationToken token = default)
        {
            var entityToUnRemove = await Get(entity.Id, true);
            entityToUnRemove.HandleNotFindEntityInRepositoryException();
            entityToUnRemove!.Remove();
            DbSetEntity.Update(entityToUnRemove);
        }

      
    }
}