using FlixOne.BookStore.Shipping.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.Shipping.DAL.Contexts
{
    public class ShipmentDbContext : DbContext
    {
        public ShipmentDbContext(DbContextOptions<ShipmentDbContext> options)
            : base(options) { }

        public ShipmentDbContext() { }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Models.Shipping> Shippings { get; set; }

    }
}