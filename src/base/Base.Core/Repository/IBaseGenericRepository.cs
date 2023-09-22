using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.Data.SqlClient;
using Base.Core.BaseEntities;
using Base.Shared.ResultUtility;
using Base.Shared;

namespace Base.Core.Repository
{

    public interface IBaseGenericRepository<Tkey, TEntity> where TEntity : BaseEntity<Tkey>
    {
        // Query
        Task<bool> Any(Expression<Func<TEntity?, bool>> filter, CancellationToken token = default);
        Task<TEntity?> Get(Tkey key, bool track = false, CancellationToken token = default);
        Task<TEntityDto> Get<TEntityDto>(Tkey key, bool track = false, CancellationToken token = default);

        Task<TEntity?> Get(Expression<Func<TEntity, bool>> filter, bool track = false, CancellationToken token = default);

        Task<TEntityDto> Get<TEntityDto>(Expression<Func<TEntity, bool>> filter, bool track = false, CancellationToken token = default);
        Task<IEnumerable<TEntity>> GetAll(string query, SqlParameter[] parameters, bool track = false, CancellationToken token = default);

        Task<IEnumerable<TEntity?>> GetAll(Expression<Func<TEntity?, bool>> filter, bool track = false,
            CancellationToken token = default);

        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity?, bool>> filter,
            Expression<Func<TEntity?, TEntity>> select, bool track = false, CancellationToken token = default);
        //



        Task<IEnumerable<TEntityDto>> GetAll<TEntityDto>(Expression<Func<TEntity?, bool>> filter,
            Expression<Func<TEntity?, TEntity>> select, bool track = false, CancellationToken token = default);


        //
        Task<IEnumerable<TEntity>> GetAll(ISpecification<TEntity> filter, bool track = false,
            CancellationToken token = default);

        Task<IEnumerable<TEntity>> GetAll(ISpecification<TEntity> filter, ISpecification<TEntity> select, bool track = false,
            CancellationToken token = default);
        Task<PaginateResultDto<TEntity>> PaginateGetAll(Expression<Func<TEntity?, bool>> filter, int page = 1, int pageSize = Constants.Page.PageSize, bool track = false,
            CancellationToken token = default);

        Task<PaginateResultDto<TEntity>> PaginateGetAll(Expression<Func<TEntity?, bool>> filter,
            Expression<Func<TEntity?, TEntity>> select, int page = 1, int pageSize = Constants.Page.PageSize, bool track = false, CancellationToken token = default);

        Task<PaginateResultDto<TEntity>> PaginateGetAll(ISpecification<TEntity> filter, ISpecification<TEntity> select,
             int page = 1, int pageSize = Constants.Page.PageSize,
            bool track = false,
            CancellationToken token = default);

        Task Create(TEntity entity, CancellationToken token = default);

        Task Update(TEntity entity, CancellationToken token = default);
        Task Remove(TEntity entity, CancellationToken token = default);
        Task UnRemove(TEntity entity, CancellationToken token = default);
    }
}