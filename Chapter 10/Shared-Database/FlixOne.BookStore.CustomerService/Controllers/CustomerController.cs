using FlixOne.BookStore.CustomerService.Extensions;
using FlixOne.BookStore.CustomerService.Models;
using FlixOne.BookStore.CustomerService.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlixOne.BookStore.CustomerService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<controller>
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult Get() => new OkObjectResult(_repository.Get().ToViewModel());

        // GET api/<controller>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id) => new OkObjectResult(_repository.Get(id).ToViewModel());

        // POST api/<controller>
        /// <summary>
        /// 
        /// </summary>
        [HttpPost("Add")]

        public void Post([FromBody]SaveCustomerViewModel sCustomer)
        {
            var customer = new CustomerViewModel
            {
                FirstName = sCustomer.FirstName,
                LastName = sCustomer.LastName,
                MemberSince = sCustomer.MemberSince,
                Wallet = sCustomer.Wallet
            };
            _repository.Add(customer.ToModel());
        }
    }
}