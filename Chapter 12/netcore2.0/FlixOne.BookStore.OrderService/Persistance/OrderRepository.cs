using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlixOne.BookStore.OrderService.Context;
using FlixOne.BookStore.OrderService.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.OrderService.Persistance
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Add(order);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.Include(x=>x.OrderDetails) .ToListAsync();
        }

        public async Task<Order> GetByAsync(Guid id)
        {
            return await _context.FindAsync<Order>(id);
        }

        public void Remove(Guid id)
        {
            var order = GetByAsync(id);
            _context.Remove(order);
        }

        public void Update(Order order)
        {
            _context.Update(order);
        }
    }
}