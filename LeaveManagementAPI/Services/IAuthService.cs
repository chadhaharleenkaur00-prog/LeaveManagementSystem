using LeaveManagementAPI.DTOs;

namespace LeaveManagementAPI.Services
{
    public interface IAuthService
    {
        Task<ServiceResult> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}