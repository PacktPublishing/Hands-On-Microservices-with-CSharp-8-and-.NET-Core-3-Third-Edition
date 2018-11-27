using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using static System.Console;

namespace FlixOne.BookStore.MessageReceiver
{
    /// <summary>
    /// Code-example is referenced to:https://github.com/Azure/azure-service-bus/tree/master/samples/DotNet/Microsoft.Azure.ServiceBus
    /// </summary>
    class Program
    {
        private static string _connectionString;
        private static string _queueName;
        static IQueueClient _client;
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = ConfigureBuilder();
            _connectionString = builder["connectionstring"];
            _queueName = builder["queuename"];

            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            _client = new QueueClient(_connectionString, _queueName);
            RegisterOnMessageHandlerAndReceiveMessages();
            WriteLine("Press any key after getting all messages.");
            ReadKey();

            await _client.CloseAsync();
        }

        static void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _client.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }
        private static IConfigurationRoot ConfigureBuilder()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
        static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            WriteLine(
                $"Received message: #{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

           await _client.CompleteAsync(message.SystemProperties.LockToken);

        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            WriteLine("Exception context for troubleshooting:");
            WriteLine($"- Endpoint: {context.Endpoint}");
            WriteLine($"- Entity Path: {context.EntityPath}");
            WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }

    }
}