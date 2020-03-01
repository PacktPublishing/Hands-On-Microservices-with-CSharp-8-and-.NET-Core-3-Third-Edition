using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlixOne.BookStore.VendorService.Models
{
    public class Offer
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string Code { get; set; }
        public decimal Discount { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(400)")]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}