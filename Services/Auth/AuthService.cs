using HelpDeskAPI.DTOs.Login;
using HelpDeskAPI.Exceptions;
using HelpDeskAPI.Interfaces.Repositories;
using HelpDeskAPI.Interfaces.Services;
using HelpDeskAPI.Services.Tickets;

namespace HelpDeskAPI.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IUserRepository userRepository,
            ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;

        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user is null)
            {
                throw new UnauthorizedException("Credenciales inválidas");
            }

            var validPassword =
                BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.PasswordHash);

            if (!validPassword)
            {
                throw new UnauthorizedException("Credenciales inválidas");
            }

            return new LoginResponseDto
            {
                Token = "JWT_PENDIENTE"
            };

        }

    }
}
