using System;

namespace FlixOne.BookStore.StoreService.Models.Relations
{
    public class ProductAttributeRelationship:BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid AttributeId { get; set; }

        public Product Product { get; set; }
        public ProductAttribute Attribute { get; set; }
    }
}
