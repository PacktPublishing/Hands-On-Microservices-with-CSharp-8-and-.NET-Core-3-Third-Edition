using System.Collections.Generic;
using System.Linq;

namespace FlixOne.BookStore.OrderService.Extensions
{
    public static class Transpose
    {
        //Order
        public static Models.Order ToModel(this Models.OrderViewModel vm)
        {
            return new Models.Order
            {
                Id = vm.OrderId,
                CustomerId = vm.CustomerId,
                Date = vm.Date,
                StatusCode = vm.StatusCode,
                StatusDesc = vm.StatusDesc,
                Tax = vm.Tax,
                Total = vm.Total,
                NetPay = vm.NetPay,
                Items = vm.Items.ToModel()
            };
        }

        public static IEnumerable<Models.Order> ToModel(this IEnumerable<Models.OrderViewModel> vm) => vm.Select(ToModel);

        public static Models.OrderViewModel ToViewModel(this Models.Order model)
        {
            return new Models.OrderViewModel
            {
                CustomerId = model.CustomerId,
                Date = model.Date,
                OrderId = model.Id,
                StatusCode = model.StatusCode,
                StatusDesc = model.StatusDesc,
                Tax = model.Tax,
                Items = model.Items.ToViewModel()
            };
        }
        public static IEnumerable<Models.OrderViewModel> ToViewModel(this IEnumerable<Models.Order> model) => model.Select(ToViewModel);

        //OrderItems
        public static Models.OrderItem ToModel(this Models.OrderItemViewModel vm)
        {
            return new Models.OrderItem
            {
                Id = vm.OrderItemId,
                OrderId = vm.OrderId,
                ProductId = vm.ProductId,
                Discount = vm.Discount,
                Name = vm.Name,
                ImagePath = vm.ImagePath,
                Qty = vm.Qty,
                Sequence = vm.Sequence,
                UnitePrice = vm.UnitePrice,
                Total = vm.Total
            };
        }
        public static IEnumerable<Models.OrderItem> ToModel(this IEnumerable<Models.OrderItemViewModel> vm) => vm.Select(ToModel);

        public static Models.OrderItemViewModel ToViewModel(this Models.OrderItem model)
        {
            return new Models.OrderItemViewModel
            {
                Discount = model.Discount,
                ImagePath = model.ImagePath,
                Name = model.ImagePath,
                OrderId = model.OrderId,
                OrderItemId = model.Id,
                ProductId = model.ProductId,
                Qty = model.Qty,
                Sequence = model.Sequence,
                UnitePrice = model.UnitePrice
            };
        }
        public static IEnumerable<Models.OrderItemViewModel> ToViewModel(this IEnumerable<Models.OrderItem> model) => model.Select(ToViewModel);
    }
}
