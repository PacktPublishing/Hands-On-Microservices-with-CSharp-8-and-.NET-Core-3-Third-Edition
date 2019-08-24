using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace FlixOne.BookStore.ProductService.IntegrationTests.Services
{
    public class ProductTest
    {
        public ProductTest()
        {
            // Arrange
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<TestStartup>();
            var server = new TestServer(webHostBuilder);
            _client = server.CreateClient();
        }

        private readonly HttpClient _client;

        [Fact]
        public async Task ReturnProductList()
        {
            // Act
            var response = await _client.GetAsync("api/product/productlist"); //change per setting
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotEmpty(responseString);
        }
    }
}