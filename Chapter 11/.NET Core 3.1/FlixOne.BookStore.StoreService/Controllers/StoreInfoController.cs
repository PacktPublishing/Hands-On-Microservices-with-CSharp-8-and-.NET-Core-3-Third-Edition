using FlixOne.BookStore.StoreService.Contexts;
using FlixOne.BookStore.StoreService.Extensions;
using FlixOne.BookStore.StoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FlixOne.BookStore.StoreService.Controllers
{
    /// <summary>
    /// Store info controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StoreInfoController : ControllerBase
    {
        private readonly StoreContext _context;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context">StoreContext</param>
        public StoreInfoController(StoreContext context)
        {
            _context = context;
        }
        // GET: api/<controller>
        /// <summary>
        /// List stores
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetStoreList()
        {
            var stores = _context.Stores;
            return Ok(stores);
        }

        /// <summary>
        /// Get store info
        /// </summary>
        /// <param name="id">Store Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoreInfo([FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            var storeInfo = await _context.Stores.FindAsync(id);

            if (storeInfo == null)
            {
                return NotFound(new ErrorResponseModel
                {
                    Message = $"No data found for id: '{id}'",
                    Code = 404,
                    Exception = "Not Found"
                });
            }
            return new OkObjectResult(storeInfo);
        }


        /// <summary>
        /// Add new store
        /// </summary>
        /// <param name="storeInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]StoreInfo storeInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            try
            {
                _context.Stores.Add(storeInfo);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetStoreInfo", new { id = storeInfo.Id }, storeInfo);
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

        }

        // PUT api/<controller>/5
        /// <summary>
        /// Udate an existing store
        /// </summary>
        /// <param name="id">Store id</param>
        /// <param name="store">Store data</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]StoreInfo store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            if (id != store.Id)
            {
                return BadRequest(new ErrorResponseModel
                {
                    Message = $"Incorrect Store Id: '{id}'",
                    Code = 400,
                    Exception = "StoreId"
                });
            }
            _context.Entry(store).State = EntityState.Modified;
            try
            {
                store.UpdatedOn = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                if (!IsStoreExist(id))
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
                Message = "New store added successfully!"
            });
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Delete an existing store
        /// </summary>
        /// <param name="id">Store Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequestDueToModelState());
            }
            var store = await _context.Stores.FindAsync(id);

            if (store == null)
            {
                return NotFound(new ErrorResponseModel
                {
                    Message = $"No data found for store id: '{id}'",
                    Code = 404,
                    Exception = "Not Found"
                });
            }
            try
            {
                _context.Stores.Remove(store);
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
                Message = "Store deleted successfully!"
            });
        }

        private bool IsStoreExist(Guid id) => _context.Stores.Any(e => e.Id == id);
    }
}
