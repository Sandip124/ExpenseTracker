using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ExpenseTracker.Common.Pagination;

namespace ExpenseTracker.Common.Repositories.Interface
{
   public interface IGenericRepository<T>
    {
        Task DeleteAsync(T entities,CancellationToken cancellationToken = default);
        Task InsertAsync(T entities,CancellationToken cancellationToken = default);
        Task UpdateAsync(T entities, CancellationToken cancellationToken = default);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate,CancellationToken cancellationToken = default);
        IQueryable<T> GetPredicatedQueryable(Expression<Func<T, bool>>? predicate);
        IQueryable<T> GetQueryable();
        Task<T?> FindAsync(int id);
        Task<T?> FindAsync(long id);
        Task<bool> CheckIfExistAsync(Expression<Func<T, bool>> predicate,CancellationToken cancellationToken = default);
        Task CommitAsync();
        Task<Pagination<T>> PaginateAsync(IQueryable<T> queryable, int page = 1, int limit = 50);
    }
}
