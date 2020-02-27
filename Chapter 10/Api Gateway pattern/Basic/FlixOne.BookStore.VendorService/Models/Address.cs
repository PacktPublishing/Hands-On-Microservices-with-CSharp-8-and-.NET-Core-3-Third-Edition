using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlixOne.BookStore.VendorService.Models
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string AddressLine1 { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string AddressLine2 { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string City { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string State { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Country { get; set; }

        [Column(TypeName = "varchar(8)")]
        public string PIN { get; set; }

        public Guid VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}