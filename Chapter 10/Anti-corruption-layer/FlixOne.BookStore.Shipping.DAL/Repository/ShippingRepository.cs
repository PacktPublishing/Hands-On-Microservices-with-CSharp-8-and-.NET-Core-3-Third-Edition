using FlixOne.BookStore.Shipping.DAL.Contexts;
using FlixOne.BookStore.Shipping.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlixOne.BookStore.Shipping.DAL.Repository
{
    public class ShippingRepository : IShippingRepository
    {
        private readonly ShipmentDbContext _context;
        public ShippingRepository() => _context = Context();
        public ShippingRepository(ShipmentDbContext context) => _context = context;

        public IEnumerable<Models.Shipping> Get() => _context.Shippings.ToList();
        public Models.Shipping ForOrder(Guid orderId) => _context.Shippings.Where(s => s.OrderId == orderId).FirstOrDefault();
        public Order AssociatedOrder(Guid orderId) => _context.Orders.Include(o => o.Items).Where(o => o.Id == orderId).FirstOrDefault();
        //Assumption - first record of Address is shipping address
        //ToDo: Add a field for shipping address
        public Address ShippingAddress(Guid customerId) => _context.Addresses.Where(a => a.CustomerId == customerId).FirstOrDefault();
        public Models.Shipping Get(Guid id) => _context.Shippings.Where(c => c.Id == id).FirstOrDefault();

        private ShipmentDbContext Context()
        {
            var contextBuilder = new DbContextOptionsBuilder<ShipmentDbContext>();
            contextBuilder.UseSqlServer("Data Source=.;Initial Catalog=FlixOneShipmentDB;Integrated Security=True;MultipleActiveResultSets=True"); //not for production
            return new ShipmentDbContext(contextBuilder.Options);
        }

    }
}
