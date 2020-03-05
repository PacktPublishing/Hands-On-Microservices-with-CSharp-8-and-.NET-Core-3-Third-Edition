using System;
using System.Collections.Generic;

namespace FlixOne.BookStore.OrderService.Persistence
{
    /// <summary>
    /// Order Repository
    /// </summary>
    public interface IOrderRepository
    {
        public IEnumerable<Models.Order> List();
        public Models.Order Get(Guid id);
        public void Add(Models.Order order);
        public void AddOrderItem(Models.OrderItem item);
    }
}
