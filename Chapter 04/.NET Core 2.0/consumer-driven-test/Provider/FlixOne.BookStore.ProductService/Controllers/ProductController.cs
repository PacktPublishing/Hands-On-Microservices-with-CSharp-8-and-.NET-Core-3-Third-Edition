using System.Collections.Generic;
using FlixOne.BookStore.ProductService.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlixOne.BookStore.ProductService.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        [Route("product/prodctlist")]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Product>))]
        public IActionResult Get() => new OkObjectResult(new Helper().GetProducts());
    }
}