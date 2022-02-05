using ExpenseTracker.Contracts.Dto.Request;
using ExpenseTracker.Contracts.Dto.Response;

namespace ExpenseTracker.Core.Services.Interface
{
    public interface IUserService
    {
        AuthenticationResponseDto Authenticate(AuthenticationRequestDto authenticateRequestDto);
    }
}