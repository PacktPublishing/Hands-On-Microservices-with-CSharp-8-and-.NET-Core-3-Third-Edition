using System.Net.Http;

namespace FlixOne.BooStore.Console
{
    class Program
    {
        //Please change the baseAddress
        private static string baseAddress = "http://localhost:44340";
        static void Main(string[] args)
        {
            try
            {
                System.Console.WriteLine("This is a testing console, you can test your APIs:");
                var list = GetProductList();
                System.Console.WriteLine($"Listing products...");
                System.Console.WriteLine(list);
                System.Console.Read();
            }
            catch (System.Exception ex)
            {
                //do something nice here
                System.Console.ForegroundColor = System.ConsoleColor.Red;
                System.Console.WriteLine($"Ensure that all services are working.\n{ex.GetType().Name}, {ex.Message} ");
                System.Console.ForegroundColor = System.ConsoleColor.White;
                System.Console.Read();
            }

        }

        private static string GetProductList()
        {
            using (var client = new HttpClient())
            {
                var reqURL = $"{baseAddress}/productlist/47bc5369-960d-446d-3b0b-08d7bb0b44b2";
                var response = client.GetAsync(reqURL).Result;
                System.Console.WriteLine($"Status: {response.StatusCode}");
                var result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }
    }
}
