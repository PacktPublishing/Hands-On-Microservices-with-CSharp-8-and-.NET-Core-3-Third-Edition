using FlixOne.BookStore.UserService.Extensions;
using FlixOne.BookStore.UserService.Helper;
using FlixOne.BookStore.UserService.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FlixOne.BookStore.UserService.Utility
{
    /// <summary>
    /// Generate jwt token
    /// </summary>
    public class TokenUtility : ITokenUtility
    {
        JwtSettingOptions _options;
        public TokenUtility(IOptions<JwtSettingOptions> options)
        {
            _options = options.Value;
            _options.ShoutOnErrors();

        }

        public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        {
            var claims = new[]
         {
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim(JwtRegisteredClaimNames.Jti, await _options.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, _options.IssuedAt.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64),
                 identity.FindFirst(Constants.Strings.JwtClaimIdentifiers.Rol),
                 identity.FindFirst(Constants.Strings.JwtClaimIdentifiers.Id)
             };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _options.ValidIssuer,
                audience: _options.ValidAudience,
                claims: claims,
                notBefore: _options.NotBefore,
                expires: _options.Expiration,
                signingCredentials: _options.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(Constants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.FlixOneApiAccess)
            });
        }

    }
}
