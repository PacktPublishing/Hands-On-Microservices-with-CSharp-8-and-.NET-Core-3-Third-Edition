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
    /// Attributes controller
    /// </summary>
    [Route("api/[controller]")]
    public class AttributesController : ControllerBase
    {
        private readonly StoreContext _context;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        public AttributesController(StoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// List Attributes
        /// </summary>
        ///  <remarks>
        ///  Example:
        ///
        ///      GET /api/Attribute
        ///  
        ///  </remarks>
        /// <returns>List of Attribute</returns>
        ///  <response code="200">List of Attribute </response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductAttribute>), 200)]
        public IEnumerable<ProductAttribute> List()
        {
            return _context.Attributes;
        }

        // GET api/<controller>/5
        /// <summary>
        /// Get Attribute by id
        /// </summary>
        /// <param name="id">Attribute id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttribute([FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }

            var categories = await _context.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound(new ErrorResponseModel
                {
                    Message = $"No data found for id: '{id}'",
                    Code = 404,
                    Exception = "Not Found"
                });
            }
            return new OkObjectResult(categories);
        }

        // POST api/<controller>
        /// <summary>
        /// Add Attribute
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ProductAttribute productAttribute)
        {
            if (productAttribute == null)
                throw new ArgumentNullException(nameof(productAttribute));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Attributes.Add(productAttribute);
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

            return CreatedAtAction("GetAttribute", new { id = productAttribute.Id }, productAttribute);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Update an existing Attribute
        /// </summary>
        /// <param name="id">Attribute id</param>
        /// <param name="productAttribute"></param>
        /// <returns>udate Attribute</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]ProductAttribute productAttribute)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            if (id != productAttribute.Id)
            {
                return BadRequest(new ErrorResponseModel
                {
                    Message = $"Incorrect Attribute Id: '{id}'",
                    Code = 400,
                    Exception = "AttributeId"
                });
            }
            _context.Entry(productAttribute).State = EntityState.Modified;
            try
            {
                productAttribute.UpdatedOn = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                if (!IsAttributeExist(id))
                {
                    return NotFound(new ErrorResponseModel
                    {
                        Message = $"No data found for Attribute id: '{id}'",
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
                Message = "New Attribute added successfully!"
            });
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Delete an existing Attribute
        /// </summary>
        /// <param name="id">Attribute id</param>
        /// <returns>Delete Attribute</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            var attribute = await _context.Categories.FindAsync(id);

            if (attribute == null)
            {
                return NotFound(new ErrorResponseModel
                {
                    Message = $"No data found for Attribute id: '{id}'",
                    Code = 404,
                    Exception = "Not Found"
                });
            }
            try
            {
                _context.Categories.Remove(attribute);
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
                Message = "Attribute deleted successfully!"
            });
        }

        private bool IsAttributeExist(Guid id) => _context.Attributes.Any(e => e.Id == id);
    }
}
