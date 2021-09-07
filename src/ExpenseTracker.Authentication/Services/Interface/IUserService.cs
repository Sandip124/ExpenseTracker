using ExpenseTracker.Web.Models;

namespace ExpenseTracker.Authentication.Services.Interface
{
    public interface IUserService
    {
        AuthenticateResponseDto Authenticate(AuthenticateRequestDto authenticateRequestDto);
    }
}