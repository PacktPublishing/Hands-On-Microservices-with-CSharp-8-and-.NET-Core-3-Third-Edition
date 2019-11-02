using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlixOne.BookStore.OrderService.Models;

namespace FlixOne.BookStore.OrderService.Persistance
{
    public interface IOrderRepository
    {
        void Add(Order order);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByAsync(Guid id);
        void Remove(Guid id);
        void Update(Order product);
    }
}