using System.Threading.Tasks;
using ExpenseTracker.Contracts.Dto.Request;
using ExpenseTracker.Contracts.Dto.Response;
using ExpenseTracker.Core.Dto;

namespace ExpenseTracker.Core.Services.Interface
{
    public interface IUserService
    {
        AuthenticationResponseDto Authenticate(AuthenticationRequestDto authenticateRequestDto);

        Task CreateUser(UserDto userDto);
    }
}