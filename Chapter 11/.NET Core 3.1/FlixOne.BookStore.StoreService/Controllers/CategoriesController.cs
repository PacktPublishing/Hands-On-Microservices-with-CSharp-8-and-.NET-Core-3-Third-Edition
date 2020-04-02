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
    /// Category controller
    /// </summary>
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly StoreContext _context;
        /// <summary>
        /// ctor
        /// </summary>
        public CategoriesController(StoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// List categories
        /// </summary>
        ///  <remarks>
        ///  Example:
        ///
        ///      GET /api/Category
        ///  
        ///  </remarks>
        /// <returns>List of Category</returns>
        ///  <response code="200">List of Category </response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Category>), 200)]
        public IEnumerable<Category> List()
        {
            return _context.Categories;
        }

        // GET api/<controller>/5
        /// <summary>
        /// Get Category by id
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute]Guid id)
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
        /// Add Category
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Category Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Categories.Add(Category);
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

            return CreatedAtAction("GetCategory", new { id = Category.Id }, Category);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Update an existing Category
        /// </summary>
        /// <param name="id">Category id</param>
        /// <param name="Category">Category data</param>
        /// <returns>udate Category</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]Category Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            if (id != Category.Id)
            {
                return BadRequest(new ErrorResponseModel
                {
                    Message = $"Incorrect Category Id: '{id}'",
                    Code = 400,
                    Exception = "CategoryId"
                });
            }
            _context.Entry(Category).State = EntityState.Modified;
            try
            {
                Category.UpdatedOn = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                if (!IsCategoryExist(id))
                {
                    return NotFound(new ErrorResponseModel
                    {
                        Message = $"No data found for Category id: '{id}'",
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
                Message = "New Category added successfully!"
            });
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Delete an existing Category
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>Delete Category</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            var Category = await _context.Categories.FindAsync(id);

            if (Category == null)
            {
                return NotFound(new ErrorResponseModel
                {
                    Message = $"No data found for Category id: '{id}'",
                    Code = 404,
                    Exception = "Not Found"
                });
            }
            try
            {
                _context.Categories.Remove(Category);
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
                Message = "Category deleted successfully!"
            });
        }

        private bool IsCategoryExist(Guid id) => _context.Addresses.Any(e => e.Id == id);
    }
}
