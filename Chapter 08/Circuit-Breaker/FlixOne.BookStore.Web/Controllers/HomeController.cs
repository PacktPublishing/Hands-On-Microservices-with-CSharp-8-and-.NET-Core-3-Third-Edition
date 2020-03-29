using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FlixOne.BookStore.Web.Models;
using FlixOne.BookStore.Web.Services;
using Polly.CircuitBreaker;

namespace FlixOne.BookStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IProductService productService, ILogger<HomeController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        public async Task<IActionResult> Details(string id = "02341321-c20b-48b1-a2be-47e67f548f0f")
        {

            try
            {
                var product = await _productService.GetProduct(new Guid(id));
                return View(product);
            }
            catch (BrokenCircuitException)
            {
                LetClientKnowThatServiceIsNotAvailable();
            }
            return View();
        }
        public async Task<IActionResult> Index()
        {
            var products = new List<ProductViewModel>();
            try
            {
                products = await _productService.ListProducts();
                return View(products);
            }
            catch (BrokenCircuitException)
            {

                LetClientKnowThatServiceIsNotAvailable();
            }
            return View(products);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void LetClientKnowThatServiceIsNotAvailable()
        {
            ViewBag.ProductServiceInActiveMsg = "Product Service is unable to serve at the moment. This error is due to Circuit-Breaker.";
        }
    }
}
