using FlixOne.BookStore.ProductService.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlixOne.BookStore.ProductService.Controllers
{
    public class EchoController : Controller
    {
        // GET api/echo
        [HttpGet]
        [Route("echo/status")]
        public IActionResult Get()
        {
            return new ObjectResult(new ServiceStatus
            {
                Up = true,
                UpSince = Helper.ServiceDeployedOn,
                Version = Constant.Version
            });
        }
    }
}