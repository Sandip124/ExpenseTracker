using ExpenseTracker.Authentication.Dto;

namespace ExpenseTracker.Authentication.Services.Interface
{
    public interface IUserService
    {
        AuthenticateResponseDto Authenticate(AuthenticateRequestDto authenticateRequestDto);
    }
}