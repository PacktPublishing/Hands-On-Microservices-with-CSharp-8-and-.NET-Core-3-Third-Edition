using System;
using System.ComponentModel;

namespace FlixOne.BookStore.Web.Models
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        public Guid VendorId { get; set; }
        [DisplayName("Name")]
        public string ProductName { get; set; }
        [DisplayName("Description")]
        public string ProductDescription { get; set; }
        [DisplayName("Icon")]
        public string ProductImage { get; set; }
        [DisplayName("Unit Price")]
        public decimal ProductPrice { get; set; }
        public Guid CategoryId { get; set; }
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        [DisplayName("Category Description")]
        public string CategoryDescription { get; set; }
    }
}
