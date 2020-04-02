using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlixOne.BookStore.StoreService.Models
{
    [Table("Store")]
    public class StoreInfo : BaseEntity
    {
        public StoreInfo()
        {
            StoreAddress = new HashSet<StoreAddress>();
            Products = new HashSet<Product>();
        }
        [Required]
        public string Name { get; set; } = "FlixOne Inc.";
        [Required]
        public string Description { get; set; } = "FlixOne Store powered by PACKT Publishing";
        public string URL { get; set; } = "https://flixone.com";
        public string Email { get; set; } = "info@flixone.com";
        public string PhoneNumber { get; set; } = "1234567890";
        //If this is a child store. The idea is on configuration basis, we can have multi-location stores with a concept of parent-chaild store
        public Guid? ParentStoreId { get; set; } 

        public ICollection<StoreAddress> StoreAddress { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
