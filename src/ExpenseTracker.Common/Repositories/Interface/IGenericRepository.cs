﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExpenseTracker.Common.Pagination;

namespace ExpenseTracker.Common.Repositories.Interface
{
   public interface IGenericRepository<T>
    {
        void Delete(T entities);
        Task DeleteAsync(T entities);
        void Insert(T entities);
        Task InsertAsync(T entities);
        void Update(T entities);
        Task UpdateAsync(T entities);
        List<T> GetAll();
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
        IQueryable<T> GetPredicatedQueryable(Expression<Func<T, bool>>? predicate);
        IQueryable<T> GetQueryable();
        T GetById(int id);
        Task<T?> GetByIdAsync(int id);

        Task<bool> CheckIfExistAsync(Expression<Func<T, bool>> predicate);
        
        Pagination<T> Paginate(IQueryable<T> queryable, int page = 1, int limit = 100);
    }
}
