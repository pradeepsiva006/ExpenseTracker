using ExpenseTracker.Common.DTOs;

namespace ExpenseTracker.Core.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<UserResponseDto> RegisterAsync(UserRequestDTO user);
        Task<UserResponseDto?> LoginAsync(UserRequestDTO user);
    }
}
