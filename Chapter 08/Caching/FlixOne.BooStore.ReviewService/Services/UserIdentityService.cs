using Microsoft.AspNetCore.Http;
using System;

namespace FlixOne.BooStore.ReviewService.Services
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly IHttpContextAccessor _context;

        public UserIdentityService(IHttpContextAccessor context) => _context = context ?? throw new ArgumentNullException(nameof(context));

        public string GetUserIdentity() => _context.HttpContext.User.FindFirst("sub").Value;
    }
}