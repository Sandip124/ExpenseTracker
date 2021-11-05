using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}