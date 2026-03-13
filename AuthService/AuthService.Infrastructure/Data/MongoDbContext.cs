using AuthService.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AuthService.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            _database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<RefreshToken> RefreshTokens => _database.GetCollection<RefreshToken>("RefreshTokens");
    }
}
