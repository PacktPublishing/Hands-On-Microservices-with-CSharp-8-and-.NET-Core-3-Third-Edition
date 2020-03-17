using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlixOne.BookStore.ProductService.Contexts;
using FlixOne.BookStore.ProductService.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.ProductService.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context) => _context = context;

        public void Add(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAll() => _context.Products.Include(c => c.Category).ToList();

        public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.Include(c => c.Category).ToListAsync();

        public Product GetBy(Guid id) => _context.Products.Include(c => c.Category).FirstOrDefault(x => x.Id == id);

        public void Remove(Guid id)
        {
            var product = GetBy(id);
            _context.Remove(product);
        }

        public void Update(Product product) => _context.Update(product);
    }
}