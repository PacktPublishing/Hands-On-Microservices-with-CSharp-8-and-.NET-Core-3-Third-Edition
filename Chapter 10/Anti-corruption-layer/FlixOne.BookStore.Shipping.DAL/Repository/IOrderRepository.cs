using FlixOne.BookStore.Shipping.DAL.Contexts;
using FlixOne.BookStore.Shipping.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlixOne.BookStore.Shipping.DAL.Repository
{
    public interface IOrderRepository
    {
        public IEnumerable<OrderItem> AssociatedItems(Guid orderId);
        public IEnumerable<Guid> OrdersForProduct(Guid productId);
        public Order Get(Guid orderId);
        public void Add(Order order);


    }
}
