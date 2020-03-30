using FlixOne.BookStore.UserService.Models.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace FlixOne.BookStore.UserService.Models
{
    /// <summary>
    /// User model
    /// </summary>
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }
        public string FirstName { get; set; }
        public string LastName { get;  set; }
        public string Gender { get; set; }
        //Location
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }


    }
}
