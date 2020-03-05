using System;
using System.Collections.Generic;
using System.Linq;
using FlixOne.BookStore.ACL;
using FlixOne.BookStore.Common.ViewModels;
using FlixOne.BookStore.ProductService.Contexts;
using FlixOne.BookStore.ProductService.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.ProductService.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;
        private readonly IReport _report;

        public ProductRepository(ProductContext context)
        {
            _context = context;
            _report = new Report();
        }

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

        public IEnumerable<Product> GetByVendorId(Guid id) => _context.Products.Include(c => c.Category).Where(x => x.VendorId == id).ToList();

        public IEnumerable<ReportData> ToReportData(Guid productId)
        {
            var shipData = _report.Get(productId);
            return shipData.Select(ShipDataToReportData).ToList();
        }

        private ReportData ShipDataToReportData(ShippingViewModel data)
        {
            return new ReportData
            {
                ShippingId = data.ShippingId,
                OrderId = data.OrderId,
                CustomerId = data.Order.CustomerId,
                ShippingStatus = data.Status,
                TrackingNumber = data.TrackingNumber,
                CustomerFullName = data.Customer.FullName,
                InvoiceNumber = data.InvoiceNumber,
                OrderDiscount = data.Order.Discount,
                OrderTax = data.Order.Tax,
                OrderTotal = data.Order.Total,
                Address = ToAddress(data.Order.ShippingAddress),
                Items = ToOrderItems(data.Order.Items)
            };
        }

        private IEnumerable<OrderItems> ToOrderItems(IEnumerable<OrderItemViewModel> items)
        {
            return items.Select(ToOrderItems);
        }
        private OrderItems ToOrderItems(OrderItemViewModel item)
        {
            var product = _context.Products.Where(p => p.Id == item.ProductId).FirstOrDefault();
            return new OrderItems
            {
                OrderItemId = item.OrderItemId,
                OrderId = item.OrderId,
                ProductId = item.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                Qty = item.Qty,
                UnitePrice = item.UnitePrice 
            };
        }

        private static ShippingAddress ToAddress(AddressViewModel address)
        {
            return new ShippingAddress
            {
                CustomerId=address.CustomerId,
                AddressId = address.AddressId,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                PIN = address.PIN,
                Country = address.Country
            };
        }
    }
}