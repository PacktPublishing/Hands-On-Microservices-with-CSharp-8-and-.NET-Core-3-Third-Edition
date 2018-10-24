using System;

namespace FlixOne.BookStore.Models
{
    public class OrderModel
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }

    }
}