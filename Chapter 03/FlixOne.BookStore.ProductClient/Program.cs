using System;
using System.Net.Http;
using System.Web;

namespace FlixOne.BookStore.ProductClient
{
    class Program
    {
        private const string ApiKey = "myAPI Key";
        private const string BaseUrl = "http://localhost:2615/api"; //Replace it with your api server uri
        static void Main(string[] args)
        {
            GetProductList("/product/GetProductAsync");
            //Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }
        private static async void GetProductList(string resource)
        {
            using (var client = new HttpClient())
            {
                var queryString = HttpUtility.ParseQueryString(string.Empty);

                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiKey);

                var uri = $"{BaseUrl}{resource}?{queryString}";

                //Get asynchronous response for further usage
                var response = await client.GetAsync(uri);
                Console.WriteLine(response);
            }
        }
    }
}
