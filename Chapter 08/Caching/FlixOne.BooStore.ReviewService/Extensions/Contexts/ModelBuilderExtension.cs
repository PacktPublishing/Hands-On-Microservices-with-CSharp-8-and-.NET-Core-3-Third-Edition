using FlixOne.BooStore.ReviewService.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BooStore.ReviewService.Extensions.Contexts
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().HasData(

                new Review
                {
                    Id = new System.Guid("7b8fd40d-03b0-459e-9fdd-99cf365161a0"),
                    ProductId = new System.Guid("0aff379a-3e94-4547-8564-7eda7cb5d3b5"),
                    Name = "Kenneth Davis",
                    Email = "kd@flixone.com",
                    Comment = "Good book to use as a ready reference."
                });

        }
    }
}
