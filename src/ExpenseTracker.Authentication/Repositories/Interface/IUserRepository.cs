using ExpenseTracker.Authentication.Entities;
using ExpenseTracker.Common.Repositories.Interface;

namespace ExpenseTracker.Authentication.Repositories.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        
    }
}