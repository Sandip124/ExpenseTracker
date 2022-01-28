using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ExpenseTracker.Common.Pagination;
using ExpenseTracker.Common.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _currentSession;

        protected GenericRepository(DbContext context)
        {
            _context = context;
            _currentSession = _context.Set<T>();
        }

        public Task DeleteAsync(T entities,CancellationToken cancellationToken = default)
        {
            _currentSession.Remove(entities);
            return _context.SaveChangesAsync(cancellationToken);
        }

        public async Task InsertAsync(T entities,CancellationToken cancellationToken = default)
        {
            await _currentSession.AddAsync(entities, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entities,CancellationToken cancellationToken = default)
        {
            _currentSession.Update(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
        }
        
        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate,CancellationToken cancellationToken = default)
        {
            predicate ??= x => true;
            return _context.Set<T>().AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
        }

        public IQueryable<T> GetQueryable()
        {
            return _currentSession.AsQueryable<T>().AsNoTracking();
        }

        public async Task<T?> FindAsync(int id)
        {
            return await _currentSession.FindAsync(id);
        }
        
        public IQueryable<T> GetPredicatedQueryable(Expression<Func<T, bool>>? predicate)
        {
            return predicate == null ? GetQueryable() : GetQueryable().Where(predicate);
        }

        public async Task<bool> CheckIfExistAsync(Expression<Func<T, bool>> predicate,CancellationToken cancellationToken = default)
        {
            return await GetPredicatedQueryable(predicate)
                .CountAsync(cancellationToken)
                .ConfigureAwait(false) != 0;
            
        }

        public async Task<Pagination<T>> PaginateAsync(IQueryable<T> queryable, int page = 1, int limit = 50)
        {
            return new Pagination<T>(
                await queryable.Skip((page - 1) * limit).Take(limit).ToListAsync(),
                await queryable.CountAsync(),
                page,
                limit
            );
        }
    }
}
