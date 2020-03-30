using FlixOne.BookStore.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FlixOne.BookStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }
        //[AuthorizeForScopes(ScopeKeySection = "ProductList:ProductListResourceId")]
        public async Task<ActionResult> Index()
        {
            var product = await _service.GetAsync();
            return View(product);
        }

    }
}
