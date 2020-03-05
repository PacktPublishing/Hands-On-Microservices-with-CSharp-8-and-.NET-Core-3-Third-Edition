using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlixOne.BookStore.Shipping.DAL.Models
{
    //[NotMapped]
    public abstract class BaseEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        [DataType(DataType.Date)]
        public DateTime? DateModified { get; set; }
    }
}
