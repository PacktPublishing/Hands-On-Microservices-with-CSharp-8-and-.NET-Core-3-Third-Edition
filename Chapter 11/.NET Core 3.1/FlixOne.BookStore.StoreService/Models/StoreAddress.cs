using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlixOne.BookStore.StoreService.Models
{
    [Table("Address")]
    public class StoreAddress:BaseEntity
    {
        public Guid? StoreId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }

        public StoreInfo Store { get; set; }
    }
}
