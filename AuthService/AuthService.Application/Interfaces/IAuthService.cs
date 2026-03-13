using AuthService.Application.Dtos;
using System.Threading.Tasks;

namespace AuthService.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> GetMeAsync(string userId);
    }
}
