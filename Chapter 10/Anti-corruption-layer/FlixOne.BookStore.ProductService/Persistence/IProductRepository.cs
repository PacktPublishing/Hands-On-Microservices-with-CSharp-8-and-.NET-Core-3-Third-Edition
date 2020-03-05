using System;
using System.Collections.Generic;
using FlixOne.BookStore.Common.ViewModels;
using FlixOne.BookStore.ProductService.Models;

namespace FlixOne.BookStore.ProductService.Persistence
{
    public interface IProductRepository
    {
        void Add(Product product);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetByVendorId(Guid id);
        Product GetBy(Guid id);
        void Remove(Guid id);
        void Update(Product product);
        public IEnumerable<ReportData> ToReportData(Guid productId);


    }
}