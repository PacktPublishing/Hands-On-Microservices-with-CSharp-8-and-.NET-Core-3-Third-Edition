using FlixOne.BookStore.StoreService.Models.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlixOne.BookStore.StoreService.Models
{
    public class Product : BaseEntity
    {
        public Product()
        {

            ProductAttributeRelationships = new HashSet<ProductAttributeRelationship>();
        }
        [Required]
        public string ShortName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Available { get; set; }
        public int QtyAvailableForSale => QtyInStock - MinReorderLevel;
        [Required]
        public int QtyInStock { get; set; }
        [Required]
        public int MinReorderLevel { get; set; } = 3;
        [Required]
        public decimal UnitPrice { get; set; }
        public int? QtyOrdered { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? StoreId { get; set; }

        public StoreInfo Store { get; set; }
        public Category Category { get; set; }

        public ICollection<ProductAttributeRelationship> ProductAttributeRelationships { get; set; }
    }
}