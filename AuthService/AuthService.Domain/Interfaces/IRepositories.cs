using AuthService.Domain.Entities;
using System.Threading.Tasks;

namespace AuthService.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(string id);
        Task<User?> GetByEmailAsync(string email);
        Task CreateAsync(User user);
    }

    public interface IRefreshTokenRepository
    {
        Task CreateAsync(RefreshToken token);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task DeleteByUserIdAsync(string userId);
    }
}
