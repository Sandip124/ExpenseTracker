using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Repositories.Interface
{
   public interface IGenericRepository<T>
    {
        void Delete(T entity);
        void Insert(T entity);
        void Update(T entity);
        List<T> GetAll();
        T GetById(long id);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
        List<T> Get(Expression<Func<T, bool>> predicate);
        Task<T> GetItemAsync(Expression<Func<T, bool>> predicate);
        Task<int> GetCountAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindAsync(long id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);  
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);  
        Task<bool> CheckIfExistAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetQueryable();
        void Dispose(); 
    }
}
