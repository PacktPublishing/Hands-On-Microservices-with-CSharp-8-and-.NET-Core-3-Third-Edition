using FlixOne.BookStore.StoreService.Extensions.Context;
using FlixOne.BookStore.StoreService.Models;
using FlixOne.BookStore.StoreService.Models.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FlixOne.BookStore.StoreService.Contexts
{
    /// <summary>Store Context</summary>
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options) { }

        public StoreContext() { }
        public DbSet<StoreInfo> Stores { get; set; }
        public DbSet<StoreAddress> Addresses { get; set; }
        public DbSet<BookType> BookTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductAttribute> Attributes { get; set; }
        public DbSet<ProductAttributeRelationship> ProductAttributeRelationships { get; set; }
        public DbSet<ProductImageRelationship> ProductImageRelationships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);//this is required when we need to set primary key for base context
            modelBuilder.Seed();
        }

        public class StoreContextDesignFactory : IDesignTimeDbContextFactory<StoreContext>
        {
            public StoreContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<StoreContext>()
                    .UseSqlServer("Server=.;Initial Catalog=FlixOne.BookStore.Services.StoreDB;Integrated Security=true");

                return new StoreContext(optionsBuilder.Options);

            }
        }
    }
}