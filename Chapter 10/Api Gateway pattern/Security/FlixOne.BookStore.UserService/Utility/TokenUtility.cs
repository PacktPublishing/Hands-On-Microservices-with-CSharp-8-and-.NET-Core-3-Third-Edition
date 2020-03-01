using FlixOne.BookStore.UserService.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FlixOne.BookStore.UserService.Utility
{
    /// <summary>
    /// Generate jwt token
    /// </summary>
    public class TokenUtility : ITokenUtility
    {
        Settings _options;
        public TokenUtility(IOptions<Settings> options)
        {
            _options = options.Value;

        }
        public string JwtSecurityTokenSerialized(JwtSecurityToken token)
        {
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public JwtSecurityToken GenerateToken(User user, bool isLong = false)
        {
            return JwtSecurityToken(user, isLong);
        }
        private JwtSecurityToken JwtSecurityToken(User user, bool isLong = false)
        {
            var jwtKey = _options.JwtKey;
            var tokenIssuer = _options.ValidIssuer;
            var audience = _options.ValidAudience;
            var notBefore = DateTime.UtcNow;
            var tokenExpiryTime = isLong ? TimeSpan.FromDays(30) : TimeSpan.FromMinutes(30);
            var expires = notBefore.Add(tokenExpiryTime);
            var key = new SymmetricSecurityKey(Convert.FromBase64String(jwtKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>(new[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, tokenIssuer),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()), //unique id
                new Claim(JwtRegisteredClaimNames.Email, user.EmailId),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixEpochDate().ToString(),
                    ClaimValueTypes.Integer64)
            });

                  

            var token = new JwtSecurityToken(tokenIssuer, audience, claims, notBefore, expires, signingCredentials);
            return token;
        }
    }
}
