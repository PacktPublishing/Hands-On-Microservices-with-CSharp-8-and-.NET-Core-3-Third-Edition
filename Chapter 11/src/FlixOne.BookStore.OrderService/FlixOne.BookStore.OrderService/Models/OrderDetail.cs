using System;

namespace FlixOne.BookStore.OrderService.Models
{
    public class OrderDetail
    {
        public Guid Id { get; set; } // Id (Primary key)
        public Guid OrderId { get; set; } // OrderId
        public Guid ProductId { get; set; } // ProductId
        public int Qty { get; set; } // Qty
        public decimal Price { get; set; } // Qty

        // Foreign keys

        /// <summary>
        ///     Parent Order pointed by [OrderDetail].([OrderId]) (FK_OrderDetail_Order)
        /// </summary>
        public virtual Order Order { get; set; } // FK_OrderDetail_Order
    }
}