using FlixOne.BookStore.OrderService.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.OrderService.Contexts
{
    /// <summary>
    /// Order Db Context
    /// </summary>
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
            : base(options)
        {
        }

        public OrderContext()
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Models.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}