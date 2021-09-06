using System;
using ExpenseTracker.Core.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExpenseTracker.Core.Pagination;
using ExpenseTracker.Infrastructure.SessionFactory;
using NHibernate;
using NHibernate.Linq;

namespace ExpenseTracker.Infrastructure.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ISession _currentSession = BaseSessionFactory.GetCurrentSession();
        public void Delete(T entities)
        {
            _currentSession.Delete(entities);
        }

        public async Task DeleteAsync(T entities)
        {
            await _currentSession.DeleteAsync(entities).ConfigureAwait(false);
        }

        public void Insert(T entities)
        {
            _currentSession.Save(entities);
        }

        public async Task InsertAsync(T entities)
        {
            await _currentSession.SaveAsync(entities).ConfigureAwait(false);
        }

        public void Update(T entities)
        {
            _currentSession.Update(entities);
        }

        public async Task UpdateAsync(T entities)
        {
            await _currentSession.UpdateAsync(entities).ConfigureAwait(false);
        }

        public IList<T> GetAll()
        {
            return _currentSession.Query<T>().ToList();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _currentSession.Query<T>().ToListAsync<T>().ConfigureAwait(false);
        }

        public IQueryable<T> GetQueryable()
        {
            return _currentSession.Query<T>();
        }

        public T GetById(int id)
        {
            return _currentSession.Get<T>(id);
        }

        public Task<T?> GetByIdAsync(int id)
        {
            return _currentSession.GetAsync<T?>(id);
        }
        
        public IQueryable<T> GetPredicatedQueryable(Expression<Func<T, bool>>? predicate)
        {
            return predicate == null ? GetQueryable() : GetQueryable().Where(predicate);
        }

        public async Task<bool> CheckIfExistAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetPredicatedQueryable(predicate)
                .CountAsync()
                .ConfigureAwait(false) != 0;
            
        }

        public Pagination<T> Paginate(IQueryable<T> queryable, int page = 1, int limit = 100)
        {
            return new Pagination<T>(
                queryable.Skip((page - 1) * limit).Take(limit).ToList(),
                queryable.Count(),
                page,
                limit
            );
        }
    }
}
