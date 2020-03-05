using System;

namespace FlixOne.BookStore.Shipping.DAL.Models
{
    public class Shipping:BaseEntity
    {
        public string StatusId { get; set; }
        public Guid OrderId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public string TrackingNumber { get; set; }
    }
}
