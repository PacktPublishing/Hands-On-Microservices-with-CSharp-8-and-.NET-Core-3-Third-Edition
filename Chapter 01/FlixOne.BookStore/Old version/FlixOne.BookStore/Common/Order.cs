using System;
using System.Collections.Generic;
using FlixOne.BookStore.Models;

namespace FlixOne.BookStore.Common
{
    public class Order : IOrder
    {
        private readonly IOrderRepository _orderRepository;

        public Order() => _orderRepository = new OrderRepository();

        public Order(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        public IEnumerable<OrderModel> Get() => _orderRepository.GetList();

        public OrderModel GetBy(Guid orderId) => _orderRepository.Get(orderId);
    }
}