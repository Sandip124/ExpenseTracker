using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Common.DBAL
{
    public class Uow : IUow
    {
        private DbContext Context;

        public Uow(DbContext context)
        {
            Context = context;
        }
        public Task CommitAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}