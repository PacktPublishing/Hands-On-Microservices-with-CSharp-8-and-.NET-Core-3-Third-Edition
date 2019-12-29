using System;
using System.Collections.Generic;
using FlixOne.BookStore.ProductService.Models;

namespace FlixOne.BookStore.ProductService.Persistence
{
    public interface IProductRepository
    {
        void Add(Product product);
        IEnumerable<Product> GetAll();
        Product GetBy(Guid id);
        void Remove(Guid id);
        void Update(Product product);
    }
}