using System;
using System.ComponentModel.DataAnnotations;

namespace FlixOne.BookStore.CustomerService.Models
{
    /// <summary>
    /// 
    /// </summary>
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
