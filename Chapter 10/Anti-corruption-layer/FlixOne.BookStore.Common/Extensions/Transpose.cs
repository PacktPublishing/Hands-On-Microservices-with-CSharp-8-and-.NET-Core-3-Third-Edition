using FlixOne.BookStore.Common.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace FlixOne.BookStore.Common.Extensions
{
    public static class Transpose
    {
        //shipping
        public static Shipping.DAL.Models.Shipping ToModel(this ShippingViewModel vm)
        {
            var model = new Shipping.DAL.Models.Shipping
            {
                Id = vm.ShippingId,
                StatusId = vm.Status,
                OrderId = vm.OrderId,
                InvoiceNumber = vm.InvoiceNumber,
                TrackingNumber = vm.TrackingNumber
            };

            return model;
        }

        public static IEnumerable<Shipping.DAL.Models.Shipping> ToModel(this IEnumerable<ShippingViewModel> vm) => vm.Select(ToModel).ToList();

        public static ShippingViewModel ToViewModel(this Shipping.DAL.Models.Shipping model)
        {
            var vm = new ShippingViewModel
            {
                ShippingId = model.Id,
                ShippedOn = model.Date,
                Status = model.StatusId,
                OrderId = model.OrderId,
                InvoiceNumber = model.InvoiceNumber,
                TrackingNumber = model.TrackingNumber
            };
            return vm;
        }
        public static IEnumerable<ShippingViewModel> ToViewModel(this IEnumerable<Shipping.DAL.Models.Shipping> model) => model.Select(ToViewModel).ToList();
        //Order

        public static OrderViewModel ToViewModel(this Shipping.DAL.Models.Order model)
        {
            var vm = new OrderViewModel
            {
                CustomerId = model.CustomerId,
                Date = model.Date,
                OrderId = model.Id,
                Discount = model.Discount,
                StatusCode = model.StatusCode,
                StatusDesc = model.StatusDesc,
                Tax = model.Tax,
                Items = ToViewModel(model.Items)

            };
            return vm;
        }
        public static IEnumerable<OrderViewModel> ToViewModel(IEnumerable<Shipping.DAL.Models.Order> models) => models.Select(ToViewModel).ToList();

        //orderItems
        public static OrderItemViewModel ToViewModel(this Shipping.DAL.Models.OrderItem model)
        {
            return new OrderItemViewModel
            {
                OrderId = model.OrderId,
                OrderItemId = model.Id,
                ProductId = model.ProductId,
                Qty = model.Qty,
                UnitePrice = model.UnitePrice
            };
        }
        public static IEnumerable<OrderItemViewModel> ToViewModel(this IEnumerable<Shipping.DAL.Models.OrderItem> models)
        {
            return models.Select(ToViewModel).ToList();
        }

        //Customer
        public static CustomerViewModel ToViewModel(this Shipping.DAL.Models.Customer model)
        {
            return new CustomerViewModel
            {
                CustomerId = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
               // Addresses = model.CustomerAddresses.ToViewModel()
            };
        }
        public static IEnumerable<CustomerViewModel> ToViewModel(this IEnumerable<Shipping.DAL.Models.Customer> models) => models.Select(ToViewModel);

        //Address
        public static AddressViewModel ToViewModel(this Shipping.DAL.Models.Address model)
        {
            return new AddressViewModel
            {
                AddressId = model.Id,
                CustomerId = model.CustomerId,
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                City = model.City,
                State = model.State,
                Country = model.Country,
                PIN = model.PIN
            };
        }

        public static IEnumerable<AddressViewModel> ToViewModel(this IEnumerable<Shipping.DAL.Models.Address> models) => models.Select(ToViewModel);
    }
}
