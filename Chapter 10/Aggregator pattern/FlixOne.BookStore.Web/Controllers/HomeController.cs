using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FlixOne.BookStore.Web.Models;
using FlixOne.BookStore.Web.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using Polly.CircuitBreaker;
using System;

namespace FlixOne.BookStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOfferService _offerService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IOfferService offerService, ILogger<HomeController> logger)
        {
            _offerService = offerService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
        
        public async Task<IActionResult> Offers([FromQuery(Name ="id1")] string dealId, [FromQuery(Name = "id2")] string vendorId)
        {
            var offers = new List<OfferViewModel>();
            try
            {
                offers = await _offerService.ListOffers(new System.Guid(dealId),new System.Guid(vendorId));
                return View(offers);
            }
            catch (BrokenCircuitException ex)
            {

                LetClientKnowThatServiceIsNotAvailable(ex);
            }
            return View(offers);
        }

        private void LetClientKnowThatServiceIsNotAvailable(Exception ex)
        {
            ViewBag.ServiceInActiveMsg = $"Please check, all services should be running. {ex.GetType().Name}, {ex.Message}";
        }
    }
}
