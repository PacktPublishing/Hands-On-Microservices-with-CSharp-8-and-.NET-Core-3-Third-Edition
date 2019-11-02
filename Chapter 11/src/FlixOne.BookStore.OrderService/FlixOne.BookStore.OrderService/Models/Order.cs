using System;
using System.Collections.Generic;

namespace FlixOne.BookStore.OrderService.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }

        public Guid Id { get; set; } // Id (Primary key)
        public DateTime Date { get; set; } // Date

        // Reverse navigation

        /// <summary>
        ///     Child OrderDetails where [OrderDetail].[OrderId] point to this entity (FK_OrderDetail_Order)
        /// </summary>
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } // OrderDetail.FK_OrderDetail_Order
    }
}