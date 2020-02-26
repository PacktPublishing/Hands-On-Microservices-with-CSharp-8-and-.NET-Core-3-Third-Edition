using System;
using System.Collections.Generic;
using FlixOne.BookStore.VendorService.Models;

namespace FlixOne.BookStore.VendorService.Persistence
{
    public interface IVendorRepository
    {
        void Add(Product product);
        IEnumerable<Product> GetAll();
        Product GetBy(Guid id);
        void Remove(Guid id);
        void Update(Product product);
    }
}