using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace FlixOne.BookStore.Client
{
    class Program
    {
        private static string baseAddress = "https://localhost:44340/";
        private static string tokenURI = "security/token/true";
        private static string productURI = "product/list";
        static void Main(string[] args)
        {
            Console.WriteLine("*** FlixOne Console Client ***");
            var jwtToken = GetJWTToken();

            var product = GetProductData(jwtToken);
            Console.WriteLine("Product Data");
            Console.WriteLine(product);
            Console.Read();
        }

        private static string GetJWTToken()
        {
            var creds = new Login
            {
                LoginId = "login@flixone.com",
                Password = "password123"
            };

            var json = JsonSerializer.Serialize(creds);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var reqURL = $"{baseAddress}{tokenURI}";
                var response = client.PostAsync(reqURL, data).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }

        private static string GetProductData(string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var reqURL = $"{baseAddress}{productURI}";
                var response = client.GetAsync(reqURL).Result;
                Console.WriteLine($"Status: {response.StatusCode}");
                var result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }
    }
    class Login
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
    }
}
