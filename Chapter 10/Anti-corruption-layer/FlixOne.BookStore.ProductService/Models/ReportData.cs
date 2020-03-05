using System;
using System.Collections.Generic;

namespace FlixOne.BookStore.ProductService.Persistence
{
    public class ReportData
    {
        public Guid ShippingId { get; set; }
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string InvoiceNumber { get; set; }
        public string TrackingNumber { get; set; }
        public string CustomerFullName { get; set; }
        public string ShippingStatus { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal OrderDiscount { get; set; }
        public decimal OrderTax { get; set; }
        public decimal OrderNetPay => OrderTotal + OrderTax - OrderDiscount;
        public ShippingAddress Address { get; set; }
        public IEnumerable<OrderItems> Items { get; set; }
    }

    public class OrderItems
    {
        public Guid OrderItemId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal UnitePrice { get; set; }
        public int Qty { get; set; }
        public decimal ItemTotal => UnitePrice * Qty;

    }

    public class ShippingAddress
    {
        public Guid AddressId { get; set; }
        public Guid CustomerId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PIN { get; set; }
    }
}