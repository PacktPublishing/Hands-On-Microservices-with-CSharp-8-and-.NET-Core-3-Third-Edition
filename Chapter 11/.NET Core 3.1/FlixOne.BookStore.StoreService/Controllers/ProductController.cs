using FlixOne.BookStore.StoreService.Contexts;
using FlixOne.BookStore.StoreService.Extensions;
using FlixOne.BookStore.StoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlixOne.BookStore.StoreService.Controllers
{
    /// <summary>
    /// Product controller
    /// </summary>
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly StoreContext _context;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        public ProductController(StoreContext context)
        {
            _context = context;
        }
        // GET: api/<controller>
        /// <summary>
        /// List products
        /// </summary>
        ///  <remarks>
        ///  Example:
        /// 
        ///      GET /api/product
        ///  
        ///  </remarks>
        /// <returns>List of product</returns>
        ///  <response code="200">List of product </response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        public IEnumerable<Product> List()
        {
            return _context.Products;
        }

        // GET api/<controller>/5
        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound(new ErrorResponseModel
                {
                    Message = $"No data found for id: '{id}'",
                    Code = 404,
                    Exception = "Not Found"
                });
            }
            return new OkObjectResult(products);
        }

        /// <summary>
        /// Get product by storeid
        /// </summary>
        /// <param name="id">store id</param>
        /// <returns></returns>
        [HttpGet("bystore/{id}")]
        public IActionResult GetProductByStore([FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }

            var products = _context.Products.Where(p => p.StoreId != null && p.StoreId.Equals(id));

            if (products == null)
            {
                return NotFound(new ErrorResponseModel
                {
                    Message = $"No data found for storeid: '{id}'",
                    Code = 404,
                    Exception = "Not Found"
                });
            }
            return new OkObjectResult(products);
        }

        // POST api/<controller>
        /// <summary>
        /// Add product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Products.Add(product);
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

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Update an existing product
        /// </summary>
        /// <param name="id">product id</param>
        /// <param name="product">product data</param>
        /// <returns>udate product</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            if (id != product.Id)
            {
                return BadRequest(new ErrorResponseModel
                {
                    Message = $"Incorrect product Id: '{id}'",
                    Code = 400,
                    Exception = "ProductId"
                });
            }
            _context.Entry(product).State = EntityState.Modified;
            try
            {
                product.UpdatedOn = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                if (!IsProductExist(id))
                {
                    return NotFound(new ErrorResponseModel
                    {
                        Message = $"No data found for product id: '{id}'",
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
                Message = "New product added successfully!"
            });
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Delete an existing product
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>Delete product</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new ErrorResponseModel
                {
                    Message = $"No data found for product id: '{id}'",
                    Code = 404,
                    Exception = "Not Found"
                });
            }
            try
            {
                _context.Products.Remove(product);
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
                Message = "Product deleted successfully!"
            });
        }

        private bool IsProductExist(Guid id) => _context.Addresses.Any(e => e.Id == id);
    }
}
