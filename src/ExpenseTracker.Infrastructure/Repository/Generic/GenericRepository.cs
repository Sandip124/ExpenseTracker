using ExpenseTracker.Core.Data;
using ExpenseTracker.Core.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Repository.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void delete(T entity)
        {
            var dbset = _appDbContext.Set<T>().Remove(entity);
            _appDbContext.SaveChanges();
        }

        public List<T> getAll()
        {
            return _appDbContext.Set<T>().ToList();
        }

        public T getById(long id)
        {
            return _appDbContext.Set<T>().Find(id);
        }

        public IQueryable<T> getQueryable()
        {
            return _appDbContext.Set<T>();
        }

        public void insert(T entity)
        {
           
           _appDbContext.Set<T>().Add(entity);
           _appDbContext.SaveChanges();
        }

        public void update(T entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            _appDbContext.Set<T>().Update(entity);
            _appDbContext.SaveChanges();
        }
    }
}
