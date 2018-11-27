using System;
using System.Collections.Generic;
using FlixOne.BookStore.ProductService.Test.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit;
using Xunit.Abstractions;

namespace FlixOne.BookStore.ProductService.Test
{
    public class ProductAPi : IDisposable
    {
        public ProductAPi(ITestOutputHelper output)
        {
            _output = output;
        }

        public virtual void Dispose()
        {
        }

        private readonly ITestOutputHelper _output;
       
        [Fact]
        public void EnsureProductApiHonoursPactWithConsumer()
        {
            //Arrange
            const string serviceUri = "http://localhost:13607";
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new CustomOutput(_output)
                }
            };
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<TestStartup>();
            using (var server = new TestServer(webHostBuilder))
            {
                //var response = server.CreateRequest($"{serviceUri}/product/productlist")
                //    .SendAsync("GET");

                //Act / Assert
                IPactVerifier pactVerifier = new PactVerifier(config);
                pactVerifier
                    .ProviderState($"{serviceUri}/echo/status")
                    .ServiceProvider("Product API", serviceUri)
                    .HonoursPactWith("Product API Consumer")
                    .PactUri($"..//consumer - driven - test//Provider//FlixOne.BookStore.ProductService.Test//pacts//product_api_consumer-product_api")
                        //.PactUri($"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}FlixOne.BookStore.ProductService.Test{Path.DirectorySeparatorChar}pacts{Path.DirectorySeparatorChar}product_api_consumer.json")
                    .Verify();
            }
        }
    }
}