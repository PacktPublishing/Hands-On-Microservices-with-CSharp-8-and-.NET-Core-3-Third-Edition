using FlixOne.BookStore.UserService.Models;
using FlixOne.BookStore.UserService.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.UserService.Extensions.Context
{
    public static class UserModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = "d332328a-1ed2-4886-85fc-d5e80af1e207",
                    UserName = "login@flixone.com",
                    NormalizedUserName = "LOGIN@FLIXONE.COM", //this is required
                    Email = "login@flixone.com",
                    NormalizedEmail = "LOGIN@FLIXONE.COM", //this is required
                    FirstName="Gaurav",
                    LastName = "Arora",
                    PasswordHash = "AQAAAAEAACcQAAAAEHIsPuo8XPOM4YzLaJQuw3N+xZ7CHZsciq4E/rmhNi8wL4BRrHW7bzdFuGbRklKJJw==",
                    SecurityStamp= "GCIH4BO2AOAABGGBBINGU7MZH6BK45SR",
                    ConcurrencyStamp= "5981500f-76cf-462b-a27b-af7dc31822b5",
                    PhoneNumber = "1234567890"
                }
            );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = new System.Guid("8e68a87e-6023-4d92-7b72-08d7d48a1c4f"), 
                    IdentityId = "d332328a-1ed2-4886-85fc-d5e80af1e207",
                    FirstName = "Gaurav",
                    LastName = "Arora",
                    Gender = "M",
                    AddressLine1 = "3710 Westwinds Dr NE #24",
                    City= "Calgary",
                    State= "Alberta",
                    Zip= "AB T3J 5H3",
                    Country="Canada"
                }
            );
        }
    }
}
