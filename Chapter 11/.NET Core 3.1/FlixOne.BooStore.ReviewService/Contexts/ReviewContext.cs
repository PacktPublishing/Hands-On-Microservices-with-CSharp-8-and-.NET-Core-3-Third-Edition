using FlixOne.BooStore.ReviewService.Models;
using Microsoft.EntityFrameworkCore;
using FlixOne.BooStore.ReviewService.Extensions.Contexts;

namespace FlixOne.BooStore.ReviewService.Contexts
{
    public class ReviewContext : DbContext
    {
        public ReviewContext() { }
        public ReviewContext(DbContextOptions<ReviewContext> options) : base(options) { }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewCache> ReviewCache {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);//this is required when we need to set primary key for base context
            modelBuilder.Seed();
        }
    }
}
