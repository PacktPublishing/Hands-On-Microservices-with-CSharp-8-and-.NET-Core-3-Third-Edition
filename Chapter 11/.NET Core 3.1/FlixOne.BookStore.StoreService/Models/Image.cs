using FlixOne.BookStore.StoreService.Models.Relations;
using System;
using System.Collections.Generic;

namespace FlixOne.BookStore.StoreService.Models
{
    public class Image : BaseEntity
    {
        public Image()
        {
            ProductImageRelationships = new HashSet<ProductImageRelationship>();
        }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public ICollection<ProductImageRelationship> ProductImageRelationships { get; set; }
    }
}
