using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using FlixOne.BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FlixOne.BookStore.Controllers
{
    public class HomeController : Controller
    {
        private const string ProductBaseUri = "http://localhost:5220"; //put this in config
        private const string OrderBaseUri = "http://localhost:5221"; //put this in config

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Store()
        {
            ViewData["Message"] = "FlixOne Book Store.";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri
                    (ProductBaseUri);
                var contentType =
                    new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var response = client.GetAsync
                    ("/api/product/productList").Result;
                var stringData = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject
                    <List<ProductViewModel>>(stringData);
                return View(data);
            }
        }

        public IActionResult Order(string id)
        {
            ViewData["Message"] = "FlixOne Book Store.";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri (OrderBaseUri);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var orderUri = $"/api/order/orderlist?id={id}";
                var response = client.GetAsync(orderUri).Result;
                var stringData = response.Content.ReadAsStringAsync().Result;
                //var data = JsonConvert.DeserializeObject
                //    <List<OrderViewModel>>(stringData);
                return new JsonResult(true);
            }
        }
    }
}