using FlixOne.BookStore.UserService.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace FlixOne.BookStore.UserService.Utility
{
    public interface ITokenUtility
    {
        string JwtSecurityTokenSerialized(JwtSecurityToken token);
        JwtSecurityToken GenerateToken(User user, bool isLong = false);
    }
}
