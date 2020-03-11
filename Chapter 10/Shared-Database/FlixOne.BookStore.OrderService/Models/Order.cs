using System;
using System.Collections.Generic;
using System.Linq;

namespace FlixOne.BookStore.OrderService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Order : BaseEntity
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }
        public Guid CustomerId { get; set; }
        public string StatusCode { get; set; }
        public string StatusDesc { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal NetPay { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}
