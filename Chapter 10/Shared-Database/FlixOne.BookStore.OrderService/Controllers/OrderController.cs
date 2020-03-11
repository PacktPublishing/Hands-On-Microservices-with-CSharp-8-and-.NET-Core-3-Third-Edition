using FlixOne.BookStore.OrderService.Extensions;
using FlixOne.BookStore.OrderService.Models;
using FlixOne.BookStore.OrderService.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FlixOne.BookStore.OrderService.Controllers
{
    /// <summary>
    /// Order Controller
    /// </summary>
    //[Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        /// <summary>
        /// Order
        /// </summary>
        /// <param name="orderRepository"></param>
        public OrderController(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        // GET: api/<controller>
        /// <summary>
        /// List all Orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List() => new OkObjectResult(_orderRepository.List().ToViewModel());

        // GET api/<controller>/5
        /// <summary>
        /// Get order for specific OrderId
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id) => new OkObjectResult(_orderRepository.Get(id).ToViewModel());

        // POST api/<controller>
        /// <summary>
        /// Add new order
        /// </summary>
        /// <param name="OViewModel">Order object</param>
        [HttpPost]
        public void Add([FromBody]OrderViewModel OViewModel) => _orderRepository.Add(OViewModel.ToModel());

        /// <summary>
        /// Add new order item to an existing order
        /// </summary>
        /// <param name="item"></param>
        [HttpPost("OrderItem")]
        public void AddItem([FromBody]OrderItemViewModel item) => _orderRepository.AddOrderItem(item.ToModel());
    }
}
