using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Common.DBAL
{
    public class UnitofWork : IUnitofWork
    {
        private readonly DbContext Context;

        public UnitofWork(DbContext context)
        {
            Context = context;
        }
        public Task CommitAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}