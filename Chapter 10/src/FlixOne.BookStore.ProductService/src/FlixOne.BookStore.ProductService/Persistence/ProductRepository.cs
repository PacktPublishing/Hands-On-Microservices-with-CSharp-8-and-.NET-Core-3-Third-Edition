using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlixOne.BookStore.ProductService.Contexts;
using FlixOne.BookStore.ProductService.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.ProductService.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            _context.AddAsync(product);
            _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include("Category").ToListAsync(); //Get category as well
        }

        public async Task<Product> GetByAsync(Guid id)
        {
            return await _context.FindAsync<Product>(id);
        }

        public void Remove(Guid id)
        {
            var product = GetByAsync(id);
            _context.Remove(product);
        }

        public void Update(Product product)
        {
            _context.Update(product);
        }
    }
}