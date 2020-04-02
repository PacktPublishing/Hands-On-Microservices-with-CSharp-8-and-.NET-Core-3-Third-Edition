using System;
using System.ComponentModel.DataAnnotations;

namespace FlixOne.BookStore.StoreService.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
    }
}
