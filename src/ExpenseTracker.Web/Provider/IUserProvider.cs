using System.Threading.Tasks;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Web.Provider
{
    public interface IUserProvider
    {
        Task<User> GetCurrentUser();
        int GetCurrentUserId();
    }
}