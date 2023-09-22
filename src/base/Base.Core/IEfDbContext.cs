using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Base.Core
{
    public interface IEfDbContext : IDisposable, IAsyncDisposable
    {
        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());


        public DbSet<TEntity> Set<TEntity>() where TEntity : class;
        public DbSet<TEntity> Set<TEntity>(string name) where TEntity : class;
        public EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;
        public EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
        public EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
        public EntityEntry Add(object entity);
        public ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default);
        public EntityEntry Attach(object entity);
        public EntityEntry Update(object entity);
        public EntityEntry Remove(object entity);
        public void AddRange(params object[] entities);
        public Task AddRangeAsync(params object[] entities);
        public void AttachRange(params object[] entities);
        public void UpdateRange(params object[] entities);
        public void RemoveRange(params object[] entities);
        public void AddRange(IEnumerable<object> entities);

        public Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default);
        public void AttachRange(IEnumerable<object> entities);

        public void UpdateRange(IEnumerable<object> entities);
        public void RemoveRange(IEnumerable<object> entities);
        public TEntity? Find<TEntity>(params object?[]? keyValues) where TEntity : class;
        public object? Find(Type entityType, params object?[]? keyValues);

        public ValueTask<object?> FindAsync(Type entityType, params object?[]? keyValues);
        public ValueTask<object?> FindAsync(Type entityType, object?[]? keyValues, CancellationToken cancellationToken);
        public ValueTask<TEntity?> FindAsync<TEntity>(params object?[]? keyValues) where TEntity : class;
        public ValueTask<TEntity?> FindAsync<TEntity>(object?[]? keyValues, CancellationToken cancellationToken) where TEntity : class;
        public IQueryable<TResult> FromExpression<TResult>(Expression<Func<IQueryable<TResult>>> expression);
    }
}