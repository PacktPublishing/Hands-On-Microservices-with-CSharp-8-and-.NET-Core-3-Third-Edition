using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FlixOne.BookStore.OrderService.Common;
using FlixOne.BookStore.OrderService.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;

namespace FlixOne.BookStore.OrderService.Clients
{
    public class ProductClient : IProductClient
    {
        public ProductClient(IOptions<AppSettings> options)
        {
            _options = options.Value;
        }
        //Using Polly to set policy:https://github.com/App-vNext/Polly
        private static readonly Policy RetryPolicy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(3,
                attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt)),
                (ex, _) => Console.WriteLine(ex.ToString()));
        private readonly AppSettings _options;
        
        

        public async Task<IEnumerable<ProductDetail>> GetProductDetailAsync(Guid productId)
        {
            var response = await RetryPolicy.ExecuteAsync(async () => await GetProductDetailsFromProductService(productId).ConfigureAwait(false));
            return await ToProductDetails(response).ConfigureAwait(false);
        }

        private async Task<IEnumerable<ProductDetail>> ToProductDetails(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var products =
                JsonConvert.DeserializeObject<List<ProductDetail>>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            return
                products.Select(p => new ProductDetail
                {
                    ProductId = p.ProductId,
                    ProductDescription = p.ProductDescription,
                    CategoryDescription = p.CategoryDescription,
                    CategoryId = p.CategoryId,
                    CategoryName = p.CategoryName,
                    ProductImage = p.ProductImage,
                    ProductName = p.ProductName,
                    ProductPrice = p.ProductPrice
                });
        }

        private async Task<HttpResponseMessage>  GetProductDetailsFromProductService(Guid productId)
        {
            var productServiceBaseUrl = _options.ProductServiceUri ?? "http: //localhost:18959/";
            var productDetailGetResource = $"api/product/productlist?id={productId}";
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(productServiceBaseUrl);
                return await httpClient.GetAsync(productDetailGetResource).ConfigureAwait(false);
            }
        }
    }
}