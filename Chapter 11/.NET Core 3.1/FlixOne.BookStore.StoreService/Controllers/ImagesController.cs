using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlixOne.BookStore.StoreService.Contexts;
using FlixOne.BookStore.StoreService.Extensions;
using FlixOne.BookStore.StoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlixOne.BookStore.StoreService.Controllers
{
    /// <summary>
    /// Image controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly StoreContext _context;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        public ImagesController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        /// <summary>
        /// List images
        /// </summary>
        ///  <remarks>
        ///  Example:
        /// 
        ///      GET /api/image
        ///  
        ///  </remarks>
        /// <returns>List of Image</returns>
        ///  <response code="200">List of Image </response>
        ///  <links>test.com</links>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Image>), 200)]
        public IEnumerable<Image> List()
        {
            return _context.Images;
        }

        // GET api/<controller>/5
        /// <summary>
        /// Get image by id
        /// </summary>
        /// <param name="id">image id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage([FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }

            var images = await _context.Images.FindAsync(id);
            if (images == null)
            {
                return NotFound(new ErrorResponseModel
                {
                    Message = $"No data found for id: '{id}'",
                    Code = 404,
                    Exception = "Not Found"
                });
            }
            return new OkObjectResult(images);
        }

        // POST api/<controller>
        /// <summary>
        /// Add address
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Image image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Images.Add(image);
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

            return CreatedAtAction("GetImage", new { id = image.Id }, image);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Update an existing image
        /// </summary>
        /// <param name="id">image id</param>
        /// <param name="image">image data</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]Image image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            if (id != image.Id)
            {
                return BadRequest(new ErrorResponseModel
                {
                    Message = $"Incorrect Image Id: '{id}'",
                    Code = 400,
                    Exception = "ImageId"
                });
            }
            _context.Entry(image).State = EntityState.Modified;
            try
            {
                image.UpdatedOn = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                if (!IsImageExist(id))
                {
                    return NotFound(new ErrorResponseModel
                    {
                        Message = $"No data found for image id: '{id}'",
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
                Message = "New image added successfully!"
            });
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Delete an existing image
        /// </summary>
        /// <param name="id">image id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            var image = await _context.Images.FindAsync(id);

            if (image == null)
            {
                return NotFound(new ErrorResponseModel
                {
                    Message = $"No data found for image id: '{id}'",
                    Code = 404,
                    Exception = "Not Found"
                });
            }
            try
            {
                _context.Images.Remove(image);
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
                Message = "Image deleted successfully!"
            });
        }

        private bool IsImageExist(Guid id) => _context.Addresses.Any(e => e.Id == id);
    }
}
