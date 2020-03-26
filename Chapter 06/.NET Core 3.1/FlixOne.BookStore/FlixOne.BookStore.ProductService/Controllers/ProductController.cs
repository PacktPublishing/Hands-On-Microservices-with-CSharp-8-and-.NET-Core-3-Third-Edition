using FlixOne.BookStore.ProductService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlixOne.BookStore.ProductService.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        Dictionary<int, Product> products = new Dictionary<int, Product>();
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            //add dummy data
            AddDummyProducts();
        }



        // GET: api/v1/product
        /// <summary>
        /// Retrieve products
        /// </summary>
        /// <returns>A response with product list</returns>
        /// <response code="200">Returns the product list</response>
        /// <response code="401">Unauthorized request</response>
        [HttpGet("productlist")]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        [ProducesResponseType(typeof(ErrorMessage),401)]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            //if (IsRequestAuthorized())
            //    return products.Values.Where(p => p.CreatedBy == User.Identity.Name).ToList();
            //return Unauthorized(new ErrorMessage { Id = Guid.NewGuid(), StatusCode = StatusCodes.Status401Unauthorized, Error = "User is not authorized." });

            return products.Values.ToList();
        }


        // GET: api/v1/Products/6243620e-5c53-42f5-85cb-69a32985ee0d
        /// <summary>
        /// Retrieves a product by ID
        /// </summary>
        /// <param name="id">Priduct id</param>
        /// <returns>A response with Product</returns>
        /// <response code="200">Returns the Product</response>
        /// <response code="401">Unauthorized request</response>
        [HttpGet("{id}")]
        public ActionResult<Product> GetSingleProduct(string id)
        {
            var productId = new Guid(id);
            if (IsRequestAuthorized())
                return products.Values.FirstOrDefault(p => p.Id == productId);
            return Unauthorized(new ErrorMessage { Id = Guid.NewGuid(), StatusCode = StatusCodes.Status401Unauthorized, Error = "User is not authorized." });
        }

        // POST api/v1/product
        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="product">Product model</param>
        /// <returns>A response with new product</returns>
        /// <response code="200">Returns the product list</response>
        /// <response code="401">Unauthorized request</response>
        [HttpPost("NewProduct")]
        public ActionResult<IEnumerable<Product>> PostProduct([FromBody]Product product)
        {
            if (IsRequestAuthorized())
            {
                var key = products.OrderByDescending(k => k.Key).FirstOrDefault().Key + 1;
                products.Add(key, new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    CreatedOn = product.CreatedOn,
                    CreatedBy = product.CreatedBy

                });
                return products.Values;
            }
            return Unauthorized(new ErrorMessage { Id = Guid.NewGuid(), StatusCode = StatusCodes.Status401Unauthorized, Error = "User is not authorized." });
        }

        private void AddDummyProducts()
        {
            if (products.Count == 0)
            {
                for (int cnt = 0; cnt < 5; cnt++)
                {
                    products.Add(cnt + 1, new Product()
                    {
                        Id = System.Guid.NewGuid(),
                        Name = $"Product:{cnt + 1}",
                        Description = $"This is a Product #{cnt + 1}",
                        Price = (cnt + 1) * 100,
                        CreatedBy = _httpContextAccessor.HttpContext.User.Identity.Name
                    });
                }
            }
        }

        private bool IsRequestAuthorized()
        {
            var authRequest = HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/scope")?.Value;
            return !string.IsNullOrEmpty(Startup.Impersonation) && authRequest != null
                    && authRequest.Split(' ').Any(s => s.Equals(Startup.Impersonation));
        }
    }
}
