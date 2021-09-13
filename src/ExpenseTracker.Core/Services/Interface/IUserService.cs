using AuthenticateRequestDto = ExpenseTracker.Core.Dto.AuthenticateRequestDto;
using AuthenticateResponseDto = ExpenseTracker.Core.Dto.AuthenticateResponseDto;

namespace ExpenseTracker.Core.Services.Interface
{
    public interface IUserService
    {
        AuthenticateResponseDto Authenticate(AuthenticateRequestDto authenticateRequestDto);
    }
}