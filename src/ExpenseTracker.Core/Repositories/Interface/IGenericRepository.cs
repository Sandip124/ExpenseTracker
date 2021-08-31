using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExpenseTracker.Core.Pagination;

namespace ExpenseTracker.Core.Repositories.Interface
{
   public interface IGenericRepository<T>
    {
        void Delete(T entities);
        Task DeleteAsync(T entities);
        void Insert(T entities);
        Task InsertAsync(T entities);
        void Update(T entities);
        Task UpdateAsync(T entities);
        IList<T> GetAll();
        Task<IList<T>> GetAllAsync();
        IQueryable<T> GetPredicatedQueryable(Expression<Func<T, bool>>? predicate);
        IQueryable<T> GetQueryable();
        T GetById(long id);
        Task<T?> GetByIdAsync(long id);

        Task<bool> CheckIfExistAsync(Expression<Func<T, bool>> predicate);
        
        Pagination<T> Paginate(IQueryable<T> queryable, int page = 1, int limit = 100);
    }
}
