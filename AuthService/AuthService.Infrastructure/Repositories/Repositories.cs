using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AuthService.Infrastructure.Data;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDbContext _context;

        public UserRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _context.Users.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)
        {
            await _context.Users.InsertOneAsync(user);
        }
    }

    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly MongoDbContext _context;

        public RefreshTokenRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(RefreshToken token)
        {
            await _context.RefreshTokens.InsertOneAsync(token);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens.Find(t => t.Token == token).FirstOrDefaultAsync();
        }

        public async Task DeleteByUserIdAsync(string userId)
        {
            await _context.RefreshTokens.DeleteManyAsync(t => t.UserId == userId);
        }
    }
}
