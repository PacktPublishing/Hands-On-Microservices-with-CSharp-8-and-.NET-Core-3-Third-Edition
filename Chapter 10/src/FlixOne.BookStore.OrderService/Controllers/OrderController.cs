using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlixOne.BookStore.OrderService.Clients;
using FlixOne.BookStore.OrderService.Models;
using FlixOne.BookStore.OrderService.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace FlixOne.BookStore.OrderService.Controllers
{
    [Route("api/Order")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductClient _productClient;

        public OrderController(IOrderRepository orderRepository, IProductClient productClient)
        {
            _orderRepository = orderRepository;
            _productClient = productClient;
        }

      
        // GET api/values
        [HttpGet]
        [Route("orderlist")]
        public async Task<IEnumerable<OrderViewModel>> ListOrder()
        {
            var orderModel = await _orderRepository.GetAllAsync();
            return orderModel.Select(ToOrderViewModel);
        }

      
        // GET api/values/5
        [HttpGet]
        [Route("order/{id}")]
        public async Task<IEnumerable<OrderViewModel>> Get(string id)
        {
            var productId = new Guid(id);
            var order = await _orderRepository.GetByAsync(productId);
            var productDetails = await _productClient.GetProductDetailAsync(productId).ConfigureAwait(false);
            return ToOrderViewModel(order, productDetails);
        }

        // POST api/values
        [HttpPost]
        [Route("addproduct")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut]
        [Route("updateproduct/{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("deleteproduct/{id}")]
        public void Delete(int id)
        {
        }

        private IEnumerable<OrderViewModel> ToOrderViewModel(Order order, IEnumerable<ProductDetail> productDetails)
        {
            var orderViewModel = new List<OrderViewModel>();
            if (order.OrderDetails.Any())
            {
                orderViewModel.AddRange(order.OrderDetails.Select(o =>
                {
                    var enumerable = productDetails as ProductDetail[] ?? productDetails.ToArray();
                    return new OrderViewModel
                    {
                        OrderId = order.Id,
                        OrderDate = order.Date,
                        Qty = o.Qty,
                        ProductId = o.ProductId,
                        ProductDescription = ProductDetail(enumerable, o).ProductDescription,
                        ProductName = ProductDetail(enumerable, o).ProductName,
                        UnitPrice = o.Price
                    };
                }));
            }
            return orderViewModel;
        }
        private OrderViewModel ToOrderViewModel(Order order)
        {
            return new OrderViewModel();
        }
        private ProductDetail ProductDetail(IEnumerable<ProductDetail> productDetails, OrderDetail o)
        {
            return productDetails.FirstOrDefault(p => p.ProductId == o.ProductId);
        }
    }
}