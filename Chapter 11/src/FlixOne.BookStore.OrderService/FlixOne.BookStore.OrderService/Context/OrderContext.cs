using FlixOne.BookStore.OrderService.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.OrderService.Context
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext>options) : base(options)
        {
        }

        public OrderContext()
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}