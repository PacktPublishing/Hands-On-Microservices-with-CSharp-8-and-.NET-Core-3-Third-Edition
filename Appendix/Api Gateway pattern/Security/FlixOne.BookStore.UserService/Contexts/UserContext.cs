using FlixOne.BookStore.UserService.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.VendorService.Contexts
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public UserContext()
        {
        }
        public DbSet<User> Users { get; set; }
    }
}