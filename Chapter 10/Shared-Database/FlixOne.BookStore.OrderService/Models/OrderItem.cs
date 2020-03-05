using System;

namespace FlixOne.BookStore.OrderService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Sequence { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal UnitePrice { get; set; }
        public decimal Discount { get; set; }
        public int Qty { get; set; }
        public decimal Total { get; set; }

    }
}
