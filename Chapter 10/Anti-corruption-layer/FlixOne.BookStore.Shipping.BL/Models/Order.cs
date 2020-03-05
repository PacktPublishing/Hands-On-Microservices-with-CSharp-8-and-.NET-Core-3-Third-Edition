using System;
using System.Collections.Generic;
using System.Text;

namespace FlixOne.BookStore.Shipping.DAL.Models
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string StatusCode {get;set;} 
        public string StatusDesc { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }

    public enum Status
    {
       Initaited = 1,
       Awaiting = 2,
       Processing = 3,
       InTransit = 4,
       Received = 5,
       Confirmed=6,
       Cancelled=7,
       Returned=8

    }
}
