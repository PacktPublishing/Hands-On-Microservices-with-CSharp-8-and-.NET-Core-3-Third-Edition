using System;
using System.Collections.Generic;
using FlixOne.BookStore.Common.Extensions;
using FlixOne.BookStore.Common.ViewModels;
using FlixOne.BookStore.Shipping.DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlixOne.BookStore.Shipping.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingController : ControllerBase
    {
        private readonly IShippingRepository _repository;
        private readonly ILogger<ShippingController> _logger;


        public ShippingController(IShippingRepository repository, ILogger<ShippingController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<ShippingViewModel> Get()
        {
            var shippings = _repository.Get().ToViewModel();
            foreach (var shipping in shippings)
            {
                shipping.Order = _repository.AssociatedOrder(shipping.OrderId).ToViewModel();
                shipping.Order.ShippingAddress =  _repository.ShippingAddress(shipping.Order.CustomerId).ToViewModel();
                
            }
            return shippings;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ShippingViewModel Get(string id)
        {
            var shipping = _repository.Get(new Guid(id)).ToViewModel();
            shipping.Order = _repository.AssociatedOrder(shipping.OrderId).ToViewModel();
            shipping.Order.ShippingAddress = _repository.ShippingAddress(shipping.Order.CustomerId).ToViewModel();
            return shipping;
        }

        [HttpGet("product/{id}")]
        public ShippingViewModel ByProduct(string id) => _repository.Get(new Guid(id)).ToViewModel();

    }
}
