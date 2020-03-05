using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlixOne.BookStore.Shipping.DAL.Models
{
    public class Address:BaseEntity
    {
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

        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
