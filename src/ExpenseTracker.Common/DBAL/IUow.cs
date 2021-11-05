using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Common.DBAL
{
    public interface IUow
    {
        Task CommitAsync();        
    }
}