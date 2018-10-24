using FlixOne.BookStore.Models;
using System;
using System.Collections.Generic;

namespace FlixOne.BookStore.Common
{
    public interface IOrder
    {
        IEnumerable<OrderModel> Get();
        OrderModel GetBy(Guid orderId);
    }
}
