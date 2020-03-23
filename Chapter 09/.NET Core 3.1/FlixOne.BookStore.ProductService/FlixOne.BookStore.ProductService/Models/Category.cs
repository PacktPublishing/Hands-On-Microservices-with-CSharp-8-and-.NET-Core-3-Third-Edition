using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FlixOne.BookStore.ProductService.Models
{
    public class Category
    {
        public Category() => Products = new List<Product>();

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public IEnumerable<Product> Products { get; set; }
    }
}