using FlixOne.BookStore.OrderService.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.OrderService.Extensions.Context
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderInfo>().HasData(
                new OrderInfo
                {
                    Id = new System.Guid("32a8e2f1-4491-4df9-a3dc-bfaa9d55165b"),
                    CustomerId = new System.Guid("8e68a87e-6023-4d92-7b72-08d7d48a1c4f"),
                    StoreId = new System.Guid("d332328a-1ed2-4886-85fc-d5e80af1e207"),
                    Paid = true,
                    Total = 0M
                });
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem
                {
                    Id = new System.Guid("794ac4f9-336e-4ebc-9090-6c3ea3b92f14"),
                    OrderId = new System.Guid("32a8e2f1-4491-4df9-a3dc-bfaa9d55165b"),
                    ProductId = new System.Guid("9a377c89-58e4-4b9f-8ef0-6ef92f902ecb"),                   
                    Name = "C# 7 and .NET: Designing Modern Cross-platform Applications",
                    Description = "Explore C# and the .NET Core framework to create applications and optimize them with ASP.NET Core 2",
                    QtyOrdered = 3,
                    UnitPrice = 365.00M
                });
        }
    }
}