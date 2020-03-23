using Newtonsoft.Json;
using ProductServiceClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ProductServiceClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("This is a console test");
            // IList<Product> products = ListUsingProductClient();
            IList<Product> products = ListUsingHttpClient();
            Console.WriteLine($"Total count {products.Count}");
            foreach (var product in products)
            {
                Console.WriteLine($"ProductId:{product.Id},Name:{product.Name}");
            }
            Console.Write("Press any key to continue ....");
            Console.ReadLine();
        }

        private static IList<Product> ListUsingHttpClient()
        {
            using (var client = new HttpClient())
            {
                var reqURL = $"http://localhost:51410/api/product";
                var response = client.GetAsync(reqURL).Result;
                System.Console.WriteLine($"Status: {response.StatusCode}");
                var result = response.Content.ReadAsStringAsync().Result;
               var products = JsonConvert.DeserializeObject<List<Product>>(result);
                return products;
            }
        }

        private static IList<Product> ListUsingProductClient()
        {
            var client = new ProductServiceClientClient { BaseUri = new Uri("http://localhost:51410/") };
            var products = client.Product.Get();
            return products;
        }
    }
}