using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlixOne.BookStore.UserService.Models
{
    /// <summary>
    /// Auth request
    /// </summary>
    public class AuthRequest
    {
        /// <summary>
        /// Login <c>id</c>, Email <c>id</c> or Mobile Number
        /// </summary>
        [Required]
        public string LoginId { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
    /// <summary>
    /// Register user
    /// </summary>
    public class RegisterUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
    }
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string UserId { get;  set; }
        public string UserType { get; set; }
        public bool IsActive { get;  set; }
    }
}
