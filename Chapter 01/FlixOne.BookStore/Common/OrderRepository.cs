using System;
using System.Collections.Generic;
using System.Linq;
using FlixOne.BookStore.Models;

namespace FlixOne.BookStore.Common
{
    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<OrderModel> GetList() => DummyData();

        public OrderModel Get(Guid orderId) => DummyData().FirstOrDefault(x => x.OrderId == orderId);

        private IEnumerable<OrderModel> DummyData()
        {
            return new List<OrderModel>
            {
                new OrderModel
                {
                    OrderId = new Guid("61d529f5-a9fd-420f-84a9-ab86f3eaf8ad"),

                    OrderDate = DateTime.Now,
                    OrderStatus = "In Transit"
                },
                new OrderModel
                {
                    OrderId = new Guid("f45eaa8b-5e07-4387-a181-aa7be7328956"),
                    OrderDate = DateTime.Now,
                    OrderStatus = "Dispatched"
                },
                new OrderModel
                {
                    OrderId = new Guid("a7e47a6f-29c4-4dbb-a778-e4cdaca2c81b"),
                    OrderDate = DateTime.Now,
                    OrderStatus = "In Factory"
                },
                new OrderModel
                {
                    OrderId = new Guid("7941ee09-1e82-41fe-bbd4-ab0d59e8cbd1"),
                    OrderDate = DateTime.Now,
                    OrderStatus = "Delivered"
                }
            };
        }
    }
}