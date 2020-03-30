using FlixOne.BookStore.UserService.Models;
using FlixOne.BookStore.UserService.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.VendorService.Contexts
{
    public class UserContext :  IdentityDbContext<AppUser>
    {
        public UserContext(DbContextOptions options)
            : base(options)
        {
        }

        public UserContext()
        {
        }
        public DbSet<User> Users { get; set; }
    }
}