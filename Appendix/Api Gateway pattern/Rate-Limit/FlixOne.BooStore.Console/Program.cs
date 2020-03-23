using System.Net.Http;

namespace FlixOne.BooStore.Console
{
    class Program
    {
        //Please change the baseAddress
        private static string baseAddress = "https://localhost:44340";
        static void Main(string[] args)
        {
            System.Console.WriteLine("This is a testing console, you can test your APIs:");
            var list = GetProductList();
            System.Console.WriteLine($"Listing products...");
            System.Console.WriteLine(list);
            System.Console.Read();
        }

        private static string GetProductList()
        {
            using (var client = new HttpClient())
            {
                var reqURL = $"{baseAddress}/product/list";
                var response = client.GetAsync(reqURL).Result;
                System.Console.WriteLine($"Status: {response.StatusCode}");
                var result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }
    }
}
