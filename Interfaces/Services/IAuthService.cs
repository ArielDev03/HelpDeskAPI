using HelpDeskAPI.DTOs.Login;

namespace HelpDeskAPI.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto dto);
    }
}
