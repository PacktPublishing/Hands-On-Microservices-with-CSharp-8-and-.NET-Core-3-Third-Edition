using FlixOne.BookStore.Shipping.DAL.Contexts;
using FlixOne.BookStore.Shipping.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlixOne.BookStore.Shipping.DAL.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShipmentDbContext _context;

        public OrderRepository() => _context = Context();
        public OrderRepository(ShipmentDbContext context) => _context = context;

        public IEnumerable<OrderItem> AssociatedItems(Guid orderId) => _context.OrderItems.Where(o => o.OrderId == orderId).ToList();
        public Order Get(Guid orderId) => _context.Orders.Include(o => o.Items).Where(o => o.Id == orderId).FirstOrDefault();
        public void Add(Order order) => _context.Add(order);
        public IEnumerable<Guid> OrdersForProduct(Guid productId) => _context.OrderItems.Where(o => o.ProductId == productId).Select(o => o.OrderId).Distinct().ToList();

        private ShipmentDbContext Context()
        {
            var contextBuilder = new DbContextOptionsBuilder<ShipmentDbContext>();
            contextBuilder.UseSqlServer("Data Source=.;Initial Catalog=FlixOneShipmentDB;Integrated Security=True;MultipleActiveResultSets=True"); //not for production
            return new ShipmentDbContext(contextBuilder.Options);
        }
    }
}
