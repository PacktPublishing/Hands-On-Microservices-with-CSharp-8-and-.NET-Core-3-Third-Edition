using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlixOne.BookStore.VendorService.Models
{
    public class Vendor
    {
        public Vendor()
        {
            Addresses = new List<Address>();
        }

        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string Code { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(400)")]
        public string Description { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string URL { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Logo { get; set; }

        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}