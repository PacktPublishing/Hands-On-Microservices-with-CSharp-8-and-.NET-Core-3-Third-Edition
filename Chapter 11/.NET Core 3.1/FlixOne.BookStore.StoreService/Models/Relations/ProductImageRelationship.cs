using System;

namespace FlixOne.BookStore.StoreService.Models.Relations
{
    public class ProductImageRelationship:BaseEntity
    {
        public Guid ImageId { get; set; }
        public Image Image { get; set; }
    }
}
