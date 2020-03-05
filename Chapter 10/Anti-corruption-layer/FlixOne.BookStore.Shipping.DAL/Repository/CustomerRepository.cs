using FlixOne.BookStore.Shipping.DAL.Contexts;
using FlixOne.BookStore.Shipping.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlixOne.BookStore.Shipping.DAL.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ShipmentDbContext _context;
        public CustomerRepository() => _context = Context();
        public CustomerRepository(ShipmentDbContext context) => _context = context;
        public IEnumerable<Customer> Get() => _context.Customers.Include(c => c.CustomerAddresses).ToList();
        public Customer Get(Guid id) => _context.Customers.Include(c => c.CustomerAddresses).Where(c => c.Id == id).FirstOrDefault();
        private ShipmentDbContext Context()
        {
            var contextBuilder = new DbContextOptionsBuilder<ShipmentDbContext>();
            contextBuilder.UseSqlServer("Data Source=.;Initial Catalog=FlixOneShipmentDB;Integrated Security=True;MultipleActiveResultSets=True"); //not for production
            return new ShipmentDbContext(contextBuilder.Options);

        }
    }
}
