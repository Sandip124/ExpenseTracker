using System.Threading.Tasks;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Web.Providers.Interface
{
    public interface IUserProvider
    {
        Task<User> GetCurrentUser();
        int GetCurrentUserId();
    }
}