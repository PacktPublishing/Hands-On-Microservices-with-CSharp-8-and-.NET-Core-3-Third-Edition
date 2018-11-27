using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FlixOne.BookStore.Client.Test.Pactconsumers;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
namespace FlixOne.BookStore.Client.Test.ConsumerTests
{
    public class ProductApiTest : IClassFixture<ConsumerProductApi>
    {
        private readonly IMockProviderService _mockProviderService;
        private readonly string _serviceBaseUri;

        public ProductApiTest(ConsumerProductApi productApi)
        {
            _mockProviderService = productApi.MockProviderService;
            _serviceBaseUri = productApi.ServiceBaseUri;
            _mockProviderService.ClearInteractions();
        }

        [Fact]
        public void WhenApiIsUp_ReturnsTrue()
        {
            //Arrange
            _mockProviderService.UponReceiving("a request to check the api status")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Headers = new Dictionary<string, object> { { "Accept", "application/json" } },
                    Path = "/echo/status"
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object> { { "Content-Type", "application/json; charset=utf-8" } },
                    Body = new
                    {
                        up = true,
                        upSince = DateTime.UtcNow,
                        version = "2.0.0",
                        message = "I'm up and running from last 19 hours."
                    }
                });

            var consumer = new ProductApiClient(_serviceBaseUri);

            //Act
            var result = consumer.ApiStatus().Up;

            //Assert
            Assert.True(result);

            _mockProviderService.VerifyInteractions();
        }

        [Fact]
        public void GetProductList()
        {
            //Arrange
            _mockProviderService.UponReceiving("a request to get list of first five product items.")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Headers = new Dictionary<string, object> {{"Accept", "application/json"}},
                    Path = "/product/productlist"
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object> {{"Content-Type", "application/json; charset=utf-8"}},
                    Body = new[]
                    {

                        new
                        {
                            id = "430c659d-0065-4abc-8bc3-9c5b30ecd717",
                            name = "Product-1",
                            description = "1 Product description.",
                            image = "",
                            price = 0

                        },
                        new
                        {
                            id = "c6502d76-83fd-44d5-b84d-987d28933f0a",
                            name = "Product-2",
                            description = "2 Product description.",
                            image = "",
                            price = 0

                        },
                        new
                        {
                            id = "932cc010-7838-4d8e-945d-41d3a08b58c0",
                            name = "Product-3",
                            description = "3 Product description.",
                            image = "",
                            price = 0

                        },
                        new
                        {
                            id = "a37fc4c3-a053-4229-81a7-44ac5db00530",
                            name = "Product-4",
                            description = "4 Product description.",
                            image = "",
                            price = 0

                        },
                        new
                        {
                            id = "e0d30816-5362-41c5-9f9a-2c1bf3c12d10",
                            name = "Product-5",
                            description = "5 Product description.",
                            image = "",
                            price = 100

                        }
                    }
                });

            var consumer = new ProductApiClient(_serviceBaseUri);

            //Act
            var result = consumer.GetProducts();

            //Assert
            Assert.True(result.Any());


            _mockProviderService.VerifyInteractions();
        }
    }
}
