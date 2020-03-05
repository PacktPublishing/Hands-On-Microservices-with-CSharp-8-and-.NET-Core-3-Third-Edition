using FlixOne.BookStore.Shipping.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;

namespace FlixOne.BookStore.Shipping.API
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ShipmentDbContext>
    {
      
        public ShipmentDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configBuilder = new ConfigurationBuilder()

                .AddJsonFile("appsettings.json")
                .Build();
            var contextBuilder = new DbContextOptionsBuilder<ShipmentDbContext>();
            var connString = configBuilder.GetConnectionString("ShipmentConnection");
            contextBuilder.UseSqlServer(connString);
            return new ShipmentDbContext(contextBuilder.Options);

        }
    }
   
}
