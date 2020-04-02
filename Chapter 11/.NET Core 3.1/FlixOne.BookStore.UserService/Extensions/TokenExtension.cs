using FlixOne.BookStore.UserService.Utility;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlixOne.BookStore.UserService.Extensions
{
    /// <summary>
    /// Token Extension
    /// </summary>
    public static class TokenExtension
    {
        /// <summary>
        /// Generate Jwt token
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="tokenUtility"></param>
        /// <param name="userName"></param>
        /// <param name="jwtOptions"></param>
        /// <param name="serializerSettings"></param>
        /// <returns>return json string</returns>
        public static async Task<string> GenerateJwtJson(this ClaimsIdentity identity, ITokenUtility tokenUtility, string userName, JwtSettingOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var jwt = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await tokenUtility.GenerateEncodedToken(userName, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(jwt, serializerSettings);
        }
    }
}
