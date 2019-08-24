using System;
using System.Collections.Generic;
using FlixOne.BookStore.ProductService.Models;

namespace FlixOne.BookStore.ProductService
{
    public class Helper
    {
        //This is the date when last build deployed on serevr
        public static DateTime ServiceDeployedOn => new DateTime(2017, 10, 1, 23, 59, 59, DateTimeKind.Utc);

        public IEnumerable<Product> GetProducts(int totalItems=5) => CreateProductList(totalItems);

        private IEnumerable<Product> CreateProductList(int maxCount)
        {
            var products = new List<Product>();
            for (var cntIndex = 1; cntIndex <= maxCount; cntIndex++)
            {
                products.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = $"Product-{cntIndex}",
                    Price = Convert.ToDecimal(cntIndex / maxCount) * 100,
                    Description = $"{cntIndex} Product description."
                });

            }

            return products;
        }
    }
}