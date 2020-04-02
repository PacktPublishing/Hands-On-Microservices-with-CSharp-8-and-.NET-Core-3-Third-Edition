using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlixOne.BookStore.StoreService.Models
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        [Required]
        public Guid BookTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public BookType BookType { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}