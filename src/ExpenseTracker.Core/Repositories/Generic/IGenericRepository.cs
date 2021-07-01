using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Repositories.Generic
{
   public interface IGenericRepository<T>
    {
        void delete(T entity);
        void insert(T entity);
        void update(T entity);
        List<T> getAll();
        T getById(long id);
        IQueryable<T> getQueryable();
    }
}
