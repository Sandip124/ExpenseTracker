using ExpenseTracker.Authentication.Entities;
using ExpenseTracker.Authentication.Repositories.Interface;

namespace ExpenseTracker.Infrastructure.Repositories.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        
    }
}