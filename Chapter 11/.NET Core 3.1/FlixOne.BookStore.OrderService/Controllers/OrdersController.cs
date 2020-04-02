using FlixOne.BookStore.OrderService.Contexts;
using FlixOne.BookStore.OrderService.Extensions;
using FlixOne.BookStore.OrderService.Models;
using FlixOne.BookStore.OrderService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace FlixOne.BookStore.OrderService.Controllers
{
    /// <summary>
    /// Order Controller
    /// </summary>
    [Authorize(Policy = "FlixOneUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderContext _context;
        private readonly IUserIdentityService _identityService;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="identityService"></param>
        public OrdersController(OrderContext context, IUserIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }
        // GET: api/<controller>
        /// <summary>
        /// List order for a customer
        /// </summary>
        /// <param name="customerID">Customer id</param>
        /// <returns>List orders</returns>
        [HttpGet("bycustomer/{customerID}")]
        public IActionResult List([FromRoute] Guid customerID)
        {
            Guid customerId = new Guid(_identityService.GetUserIdentity());
            var orders = _context.OrderInfos.Where(o => o.CustomerId == customerID);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            if (orders == null)
            {
                return new NotFoundObjectResult(new ErrorResponseModel
                {
                    Message = $"No Order found for customer Id:'{customerID}'",
                    Code = 404,
                    Exception = "Not found"
                });
            }
            return new OkObjectResult(orders);
        }

        /// <summary>
        /// Fetch order
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <returns>Fetch orer</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            var orders = await _context.OrderInfos.FindAsync(id);
            if (orders == null)
            {
                return new NotFoundObjectResult(new ErrorResponseModel
                {
                    Message = $"No Order found for OrderId:'{id}'",
                    Code = 404,
                    Exception = "Not found"
                });
            }
            return new OkObjectResult(orders);

        }

        /// <summary>
        /// Place a new order
        /// </summary>
        /// <param name="order">Order data</param>
        /// <returns>Add a new order</returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]OrderInfo order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            try
            {
                _context.OrderInfos.Add(order);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                if (IsOrderExist(order.Id))
                {
                    return new ConflictObjectResult(new ErrorResponseModel
                    {
                        Message = dbEx.Message,
                        Code = 400,
                        Exception = dbEx.GetType().Name
                    });
                }
                return new BadRequestObjectResult(new ErrorResponseModel
                {
                    Message = dbEx.Message,
                    Code = 400,
                    Exception = dbEx.GetType().Name
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new ErrorResponseModel
                {
                    Message = ex.Message,
                    Code = 400,
                    Exception = ex.GetType().Name
                });
            }
            return CreatedAtAction("GetOrder", new { id = order.Id }, order);


        }

        private bool IsOrderExist(Guid Id) => _context.OrderInfos.Any(o => o.Id == Id);
    }
}
