using System;

namespace AuthService.Domain.Entities
{
    public class RefreshToken
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
    }
}
