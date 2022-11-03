using Microsoft.EntityFrameworkCore;
using SharedKernels.interfaces.BaseInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernels
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbContext dbContext;

        public RepositoryBase(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <inheritdoc/>
        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await dbContext.Set<T>().AddAsync(entity);

            await SaveChangesAsync(cancellationToken);

            return entity;
        }
        public virtual async Task<List<T>> AddRangeAsync(List<T> entity, CancellationToken cancellationToken = default)
        {
            await dbContext.Set<T>().AddRangeAsync(entity);

            await SaveChangesAsync(cancellationToken);

            return entity;
        }
        /// <inheritdoc/>
        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbContext.Entry(entity).State = EntityState.Modified;

            await SaveChangesAsync(cancellationToken);
        }
        /// <inheritdoc/>
        public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbContext.Set<T>().Remove(entity);

            await SaveChangesAsync(cancellationToken);
        }
        /// <inheritdoc/>
        public virtual async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            dbContext.Set<T>().RemoveRange(entities);

            await SaveChangesAsync(cancellationToken);
        }
        /// <inheritdoc/>
        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        /// <inheritdoc/>
        public virtual async Task<T> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await dbContext.Set<T>().FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }
        /// <inheritdoc/>
        public virtual async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
        }
        /// <inheritdoc/>
        public virtual IQueryable<T> WhereAsync(Expression<Func<T, bool>> filter)
        {
            return dbContext.Set<T>().Where(filter);
        }

        public virtual async Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await dbContext.Set<T>().FirstOrDefaultAsync(cancellationToken);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
