using System;
using System.Collections.Generic;

namespace FlixOne.BookStore.OrderService.Models
{
    public class OrderInfo
    {
        public OrderInfo()
        {
            OrderItems = new HashSet<OrderItem>();
        }
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? StoreId { get; set; }
        public decimal Total { get; set; }
        public bool Paid { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
