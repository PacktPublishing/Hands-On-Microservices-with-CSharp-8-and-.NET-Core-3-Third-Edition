using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using FlixOne.BookStore.ProductService.Contexts;
using FlixOne.BookStore.ProductService.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.ProductService.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context) => _context = context;

        public IObservable<IEnumerable<Product>> GetAll() => Observable.Return(GetProducts());

        public IObservable<IEnumerable<Product>> GetAll(IScheduler scheduler) => Observable.Return(GetProducts(), scheduler);

        public IObservable<Unit> Remove(Guid productId) => Remove(productId, null);

        public IObservable<Unit> Remove(Guid productId, IScheduler scheduler)
        {
            DeleteProduct(productId);

            return scheduler != null
                ? Observable.Return(new Unit(), scheduler)
                : Observable.Return(new Unit());
        }

        private IEnumerable<Product> GetProducts()
        {
            var products = (from p in _context.Products.Include(p => p.Category)
                            orderby p.Name
                            select p).ToList();
            return products;
        }

        private Product GetBy(Guid id) => GetProducts().FirstOrDefault(x => x.Id == id);

        private void DeleteProduct(Guid productId)
        {
            var product = GetBy(productId);
            _context.Entry(product).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}