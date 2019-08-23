using FlixOne.BookStore.Models;
using System;
using System.Collections.Generic;

namespace FlixOne.BookStore.Common
{
    public interface IOrderRepository
    {
        IEnumerable<OrderModel> GetList();
        OrderModel Get(Guid orderId);
    }
}