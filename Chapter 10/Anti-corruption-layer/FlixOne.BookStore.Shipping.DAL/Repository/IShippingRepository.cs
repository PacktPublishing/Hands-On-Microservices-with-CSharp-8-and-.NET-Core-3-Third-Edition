using FlixOne.BookStore.Shipping.DAL.Models;
using System;
using System.Collections.Generic;

namespace FlixOne.BookStore.Shipping.DAL.Repository
{
    public interface IShippingRepository
    {
        public IEnumerable<Models.Shipping> Get();
        public Models.Shipping ForOrder(Guid orderId);
        public Order AssociatedOrder(Guid orderId);
        public Models.Shipping Get(Guid id);
        public Address ShippingAddress(Guid customerId);
    }
}
