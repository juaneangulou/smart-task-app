namespace SmartTaskApp.CommonDb.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; private set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsRevoked => RevokedAt.HasValue;
        public bool IsActive => !IsExpired && !IsRevoked;

        public RefreshToken(string userId)
        {
            Id = Guid.NewGuid();
            Token = Guid.NewGuid().ToString();
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
            ExpiresAt = DateTime.UtcNow.AddDays(7); 
        }

        public void Revoke()
        {
            RevokedAt = DateTime.UtcNow;
        }
    }
}
