using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace FlixOne.BookStore.UserService.Utility
{
    /// <summary>
    /// Jwt settings
    /// </summary>
    public class JwtSettingOptions
    {
        public string ValidIssuer { get; set; }
        public string Subject { get; set; }
        public string ValidAudience { get; set; }
        public string JwtKey;
        public DateTime Expiration => IssuedAt.Add(ValidFor);
        public DateTime NotBefore => DateTime.UtcNow;
        public DateTime IssuedAt => DateTime.UtcNow;
        public int TokenExpiryTimeInMinutes { get; set; } = 120;
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(120);
        public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());
        public SigningCredentials SigningCredentials { get; set; }
    }
}
