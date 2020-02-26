using System;
using System.Collections.Generic;
using System.Linq;
using FlixOne.BookStore.VendorService.Contexts;
using FlixOne.BookStore.VendorService.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.VendorService.Persistence
{
    public class VendorRepository : IVendorRepository
    {
        private readonly ProductContext _context;

        public VendorRepository(ProductContext context) => _context = context;

        public IEnumerable<Product> GetAll() => _context.Products.Include(c => c.Category).ToList();

        public Product GetBy(Guid id) => _context.Products.Include(c => c.Category).FirstOrDefault(x => x.Id == id);

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
        }

        public void Remove(Guid id)
        {
            var product = GetBy(id);
            _context.Remove(product);
            _context.SaveChanges();
        }
    }
}