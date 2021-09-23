using ExpenseTracker.Common.Repositories.Interface;
using User = ExpenseTracker.Core.Entities.User;

namespace ExpenseTracker.Core.Repositories.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        
    }
}