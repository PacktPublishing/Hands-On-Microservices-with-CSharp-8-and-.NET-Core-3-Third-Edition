using FlixOne.BookStore.OrderService.Extensions.Context;
using FlixOne.BookStore.OrderService.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.OrderService.Contexts
{
    public class OrderContext : DbContext
    {
        public OrderContext()
        {
        }

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<OrderInfo> OrderInfos { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}