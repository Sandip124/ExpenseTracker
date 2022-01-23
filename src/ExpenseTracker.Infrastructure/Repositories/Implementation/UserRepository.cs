using System.Threading.Tasks;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories.Implementation
{
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository(DbContext context) : base(context)
        {
        }
        
        public async Task<bool> UserExists(string username)
            => await CheckIfExistAsync(u => u.Username.ToLower().Trim() == username.ToLower().Trim());
    }
}