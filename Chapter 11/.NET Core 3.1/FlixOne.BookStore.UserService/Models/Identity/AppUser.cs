using Microsoft.AspNetCore.Identity;

namespace FlixOne.BookStore.UserService.Models.Identity
{
    public class AppUser : IdentityUser
    {
        // Extended Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? FacebookId { get; set; }
        public string PictureUrl { get; set; }
    }
}
