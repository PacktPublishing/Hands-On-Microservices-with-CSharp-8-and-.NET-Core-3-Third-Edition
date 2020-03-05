using System;
using System.Collections.Generic;

namespace FlixOne.BookStore.Common.ViewModels
{
    public class ShippingViewModel
    {
        public Guid ShippingId { get; set; }
        public string InvoiceNumber { get; set; }
        public string TrackingNumber { get; set; }
        public string Status { get; set; }
        public DateTime ShippedOn { get; set; }
        public Guid OrderId { get; set; }
        public CustomerViewModel Customer { get; set; }
        public OrderViewModel Order { get; set; }
    }

    public class OrderViewModel
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string StatusCode { get; set; }
        public string StatusDesc { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal NetPay => Total + Tax - Discount;
        public IEnumerable<OrderItemViewModel> Items { get; set; }
        public AddressViewModel ShippingAddress { get; set; }
    }
    public class OrderItemViewModel
    {
        public Guid OrderItemId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal UnitePrice { get; set; }
        public int Qty { get; set; }
        public decimal ItemTotal => UnitePrice * Qty;

    }

    public class CustomerViewModel
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => LastName + ", " + FirstName;
    }

    public class AddressViewModel
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
