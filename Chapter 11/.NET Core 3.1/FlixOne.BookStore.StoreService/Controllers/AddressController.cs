using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlixOne.BookStore.StoreService.Contexts;
using FlixOne.BookStore.StoreService.Extensions;
using FlixOne.BookStore.StoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.StoreService.Controllers
{
    /// <summary>
    /// Address controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly StoreContext _context;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        public AddressController(StoreContext context)
        {
            _context = context;
        }
        // GET: api/<controller>
        /// <summary>
        /// List addresses
        /// </summary>
        ///  <remarks>
        ///  Example:
        /// 
        ///      GET /api/address
        ///  
        ///  </remarks>
        /// <returns>List of addresses</returns>
        ///  <response code="200">List of addresses </response>
        ///  <links>test.com</links>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StoreAddress>), 200)]
        public IEnumerable<StoreAddress> List()
        {
            return _context.Addresses;
        }

        // GET api/<controller>/5
        /// <summary>
        /// Get address by id
        /// </summary>
        /// <param name="id">address id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddress([FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }

            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound(new ErrorResponseModel
                {
                    Message = $"No data found for id: '{id}'",
                    Code = 404,
                    Exception = "Not Found"
                });
            }
            return new OkObjectResult(address);
        }

        // POST api/<controller>
        /// <summary>
        /// Add address
        /// </summary>
        /// <param name="storeAddress">address data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]StoreAddress storeAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Addresses.Add(storeAddress);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorResponseModel
                {
                    Message = ex.Message,
                    Code = 400,
                    Exception = ex.GetType().Name
                });
            }

            return CreatedAtAction("GetAddress", new { id = storeAddress.Id }, storeAddress);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Update an existing address
        /// </summary>
        /// <param name="id">address id</param>
        /// <param name="address">address data</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]StoreAddress address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            if (id != address.Id)
            {
                return BadRequest(new ErrorResponseModel
                {
                    Message = $"Incorrect Address Id: '{id}'",
                    Code = 400,
                    Exception = "AddressId"
                });
            }
            _context.Entry(address).State = EntityState.Modified;
            try
            {
                address.UpdatedOn = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                if (!IsAddressExist(id))
                {
                    return NotFound(new ErrorResponseModel
                    {
                        Message = $"No data found for store id: '{id}'",
                        Code = 404,
                        Exception = "Not Found"
                    });
                }
                return BadRequest(new ErrorResponseModel
                {
                    Message = dbEx.Message,
                    Code = 400,
                    Exception = dbEx.GetType().Name
                });

            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorResponseModel
                {
                    Message = ex.Message,
                    Code = 400,
                    Exception = ex.GetType().Name
                });
            }
            return new OkObjectResult(new SuccessResponseModel
            {
                Code = 200,
                Message = "New address added successfully!"
            });
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Delete an existing address
        /// </summary>
        /// <param name="id">address id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound(new ErrorResponseModel
                {
                    Message = $"No data found for address id: '{id}'",
                    Code = 404,
                    Exception = "Not Found"
                });
            }
            try
            {
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorResponseModel
                {
                    Message = ex.Message,
                    Code = 400,
                    Exception = ex.GetType().Name
                });
            }

            return new OkObjectResult(new SuccessResponseModel
            {
                Code = 200,
                Message = "Address deleted successfully!"
            });
        }

        private bool IsAddressExist(Guid id) => _context.Addresses.Any(e => e.Id == id);
    }
}
