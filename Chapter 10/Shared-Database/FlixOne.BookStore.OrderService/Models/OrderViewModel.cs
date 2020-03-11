using System;
using System.Collections.Generic;
using System.Linq;

namespace FlixOne.BookStore.OrderService.Models
{
    public class OrderViewModel
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string StatusCode { get; set; }
        public string StatusDesc { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get { return Items.Sum(i => i.Total); } }
        public decimal Tax { get; set; }
        public decimal WalletBalance { get; set; }
        public decimal NetPay { get { return (Total + Tax) - WalletBalance; } }
        public IEnumerable<OrderItemViewModel> Items { get; set; }
    }
    public class OrderItemViewModel
    {
        public Guid OrderItemId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Sequence { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal UnitePrice { get; set; }
        public decimal Discount { get; set; }
        public int Qty { get; set; }
        public decimal Total { get { return (Qty * UnitePrice) - Discount; } }

    }
}
