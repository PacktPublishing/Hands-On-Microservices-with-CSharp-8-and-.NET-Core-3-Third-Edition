using System;

namespace FlixOne.BookStore.OrderService.Models
{
    public class OrderViewModel
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Qty { get; set; }
        public string Unit => Qty > 1 ? "Nos." : "No."; //We have only one unit
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Qty * UnitPrice;
    }
}