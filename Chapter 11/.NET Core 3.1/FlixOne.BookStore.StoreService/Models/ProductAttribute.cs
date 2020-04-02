using FlixOne.BookStore.StoreService.Models.Relations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlixOne.BookStore.StoreService.Models
{
    public class ProductAttribute:BaseEntity
    {
        public ProductAttribute()
        {
            ProductAttributeRelationships = new HashSet<ProductAttributeRelationship>();
        }
        public string Icon { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<ProductAttributeRelationship> ProductAttributeRelationships { get; set; }
    }
}
