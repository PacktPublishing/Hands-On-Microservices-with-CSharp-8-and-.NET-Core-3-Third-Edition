using System;

namespace FlixOne.BookStore.Shipping.DAL.Models
{
    public class OrderItem:BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal UnitePrice { get; set; }
        public int Qty { get; set; }

    }
}