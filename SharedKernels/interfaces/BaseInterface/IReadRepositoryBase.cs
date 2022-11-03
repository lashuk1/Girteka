using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernels.interfaces.BaseInterface
{   
    public interface IReadRepositoryBase<T> where T : class
    {
        Task<T> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;

        Task<List<T>> ListAsync(CancellationToken cancellationToken = default);

        IQueryable<T> WhereAsync(Expression<Func<T, bool>> filter);

        Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken = default);
    }
}
