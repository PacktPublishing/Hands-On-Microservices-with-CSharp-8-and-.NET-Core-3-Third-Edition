using FlixOne.BookStore.CustomerService.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.CustomerService.Contexts
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public CustomerContext()
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Address> Addresses { get; set; }



    }
}