using FlixOne.BookStore.ProductService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlixOne.BookStore.ProductService.Persistence
{
    public interface IProductRepository
    {
        void Add(Product product);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByAsync(Guid id);
        void Remove(Guid id);
        void Update(Product product);
    }
}
