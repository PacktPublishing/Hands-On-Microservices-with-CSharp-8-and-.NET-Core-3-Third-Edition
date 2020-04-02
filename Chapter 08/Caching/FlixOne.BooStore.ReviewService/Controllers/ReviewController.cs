using FlixOne.BookStore.StoreService.Extensions;
using FlixOne.BooStore.ReviewService.Contexts;
using FlixOne.BooStore.ReviewService.Models;
using FlixOne.BooStore.ReviewService.Repository.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlixOne.BooStore.ReviewService.Controllers
{
    /// <summary>
    /// Review Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewContext _context;
        private readonly IDistributedCache _cache;
        private readonly string Key = "FlixOneReviewKey";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cache"></param>
        public ReviewController(ReviewContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // GET: api/<controller>
        /// <summary>
        /// Get review
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>List of review</returns>
        [HttpGet("{productId}")]
        public async Task<IEnumerable<Review>> Get([FromRoute] Guid productId)
        {
            IEnumerable<Review> reviews;
            if (!string.IsNullOrEmpty(Key))
            {
                reviews = await _cache.GetAsync<IEnumerable<Review>>(Key);
            }
            else
            {
                reviews = _context.Reviews.Where(p => p.ProductId.Equals(productId));
                await _cache.SetAsync(Key, reviews, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2) });
            }
            return reviews;
        }

        
        /// <summary>
        /// Add new review
        /// </summary>
        /// <param name="review">review data</param>
        /// <returns>Newly added review</returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            _context.Reviews.Add(review);

            try
            {
                await _context.SaveChangesAsync();
                //make cache null
                _cache.Remove(Key);

            }
            catch (DbUpdateException dbE)
            {
                if (IsReviewExist(review.Id))
                {
                    return new ConflictObjectResult(new ErrorResponseModel
                    {
                        Message = dbE.Message,
                        Code = 409,
                        Exception = dbE.GetType().Name
                    });
                }
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

            return CreatedAtAction("Get", new { productId = review.ProductId }, review);
        }

        

        /// <summary>
        /// Delete an existing review
        /// </summary>
        /// <param name="id">Review id</param>
        /// <returns>Deleted review data</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }

            var reviews = await _context.Reviews.FindAsync(id);
            if (reviews == null)
            {
                return new NotFoundObjectResult(new ErrorResponseModel
                {
                    Message = "No review found for id:'{id}'",
                    Code = 404,
                    Exception = "Not found!"
                });
            }

            _context.Reviews.Remove(reviews);
            await _context.SaveChangesAsync();

            return Ok(reviews);
        }
        private bool IsReviewExist(Guid id) => _context.Reviews.Any(e => e.Id == id);
    }
}