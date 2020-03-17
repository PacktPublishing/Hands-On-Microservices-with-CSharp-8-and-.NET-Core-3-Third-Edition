using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlixOne.BookStore.UserService.Models
{
    /// <summary>
    /// User model
    /// </summary>
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string SecretKey { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
    }
}
