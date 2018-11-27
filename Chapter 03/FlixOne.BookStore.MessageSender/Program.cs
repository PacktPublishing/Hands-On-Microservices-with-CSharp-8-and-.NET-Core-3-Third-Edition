using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using static System.Console;


namespace FlixOne.BookStore.MessageSender
{
    internal class Program
    {
        private static string _connectionString;

        private static string _queuename;
        private static IQueueClient _client;
        public static IConfigurationRoot Configuration { get; set; }

        private static void Main(string[] args)
        {
            var builder = ConfigureBuilder();
            _connectionString = builder["connectionstring"];
            _queuename = builder["queuename"];

            MainAsync().GetAwaiter().GetResult();
        }

        private static IConfigurationRoot ConfigureBuilder()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        private static async Task MainAsync()
        {
            const int numberOfMessagesToSend = 10;
            _client = new QueueClient(_connectionString, _queuename);
            WriteLine("Starting...");
            await SendMessagesAsync(numberOfMessagesToSend);
            WriteLine("Ending...");
            WriteLine("Press any key...");
            ReadKey();
            await _client.CloseAsync();
        }

        private static async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {
                for (var index = 0; index < numberOfMessagesToSend; index++)
                {
                    var customMessage = $"#{index}:A message from FlixOne.BookStore.MessageSender.";
                    var message = new Message(Encoding.UTF8.GetBytes(customMessage));
                    WriteLine($"Sending message: {customMessage}");
                    await _client.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                WriteLine($"Weird! It's exception with message:{exception.Message}");
            }
        }
    }
}