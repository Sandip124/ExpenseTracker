using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExpenseTracker.Common.Pagination;
using ExpenseTracker.Common.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DbContext _context;
        private DbSet<T> _currentSession;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _currentSession = _context.Set<T>();
        }

        public void Delete(T entities)
        {
            _currentSession.Remove(entities);
        }

        public void Insert(T entities)
        {
            _currentSession.Add(entities);
        }

        public async Task InsertAsync(T entities)
        {
            await _currentSession.AddAsync(entities).ConfigureAwait(false);
        }

        public void Update(T entities)
        {
            _currentSession.Update(entities);
        }

        public IList<T> GetAll()
        {
            return _currentSession.ToList();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _currentSession.ToListAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return _currentSession.AsQueryable();
        }

        public T GetById(long id)
        {
            return _currentSession.Find(id);
        }

        public virtual async Task<T> GetByIdAsync(long id)
        {
            return await _currentSession.FindAsync(id);
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
