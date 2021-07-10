using ExpenseTracker.Core.Data;
using ExpenseTracker.Core.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<T>();
        }
        public void Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
        }

        public List<T> GetAll()
        {
            return _appDbContext.Set<T>().ToList();
        }

        public T GetById(long id)
        {
            return _appDbContext.Set<T>().Find(id);
        }

        public async Task InsertAsync(T entity)
        {
            await _appDbContext.AddAsync(entity).ConfigureAwait(false);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
        {
            predicate ??= x => true;
            return await _appDbContext.Set<T>().Where(predicate).ToListAsync().ConfigureAwait(false);
        }

        public List<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _appDbContext.Set<T>().Where(predicate).ToList();
        }

        public async Task<T> GetItemAsync(Expression<Func<T, bool>> predicate)
        {
            return await _appDbContext.Set<T>().FirstOrDefaultAsync(predicate).ConfigureAwait(false);
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>>? predicate)
        {
            predicate ??= x => true;
            return await _appDbContext.Set<T>().CountAsync(predicate).ConfigureAwait(false);
        }

        public async Task<T> FindAsync(long id)
        {
            return await _dbSet.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<bool> CheckIfExistAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate).ConfigureAwait(false);
        }

        public IQueryable<T> GetQueryable()
        {
            return _appDbContext.Set<T>();
        }

        public void Insert(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
            _appDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            _appDbContext.Set<T>().Update(entity);
            _appDbContext.SaveChanges();
        }
    }
}
