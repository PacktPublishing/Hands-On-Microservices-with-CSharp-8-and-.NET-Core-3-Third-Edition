using FlixOne.BookStore.UserService.Extensions.Context;
using FlixOne.BookStore.UserService.Models;
using FlixOne.BookStore.UserService.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.VendorService.Contexts
{
    public class UserContext : IdentityDbContext<AppUser>
    {
        public UserContext(DbContextOptions options)
            : base(options) { }

        public UserContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);//this is required when we need to set primary key for base context
            modelBuilder.Seed();
        }
        public DbSet<User> Users { get; set; }
    }
}