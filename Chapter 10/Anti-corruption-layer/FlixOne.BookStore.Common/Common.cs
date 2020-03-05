using FlixOne.BookStore.Common.ViewModels;
using FlixOne.BookStore.Shipping.DAL.Repository;
using FlixOne.BookStore.Common.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;

namespace FlixOne.BookStore.Common
{
    public class Common : ICommon
    {
        private readonly IShippingRepository _shippingRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        public Common()
        {
            _shippingRepository = new ShippingRepository();
            _customerRepository = new CustomerRepository();
            _orderRepository = new OrderRepository();
        }
        public Common(IShippingRepository repository, ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _shippingRepository = repository;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }
        public IEnumerable<ShippingViewModel> Get(Guid productId)
        {

            var orderIds = _orderRepository.OrdersForProduct(productId);
            var orders = orderIds.Select(orderId => _shippingRepository.AssociatedOrder(orderId));


            var shippingData = orderIds.Select(orderId => _shippingRepository.ForOrder(orderId)).ToViewModel();
            var sVM = new List<ShippingViewModel>();
            foreach (var item in shippingData)
            {
                item.Order = _orderRepository.Get(item.OrderId).ToViewModel();
                item.Customer = _customerRepository.Get(item.Order.CustomerId).ToViewModel();
                item.Order.ShippingAddress = _shippingRepository.ShippingAddress(item.Order.CustomerId).ToViewModel();
                sVM.Add(item);
            }
            return sVM;
        }
    }

    public interface ICommon
    {
        IEnumerable<ShippingViewModel> Get(Guid productId);
    }
}
