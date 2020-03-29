using FlixOne.BookStore.Web.Config;
using FlixOne.BookStore.Web.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FlixOne.BookStore.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        private readonly IOptions<Settings> _settings;
        private readonly string _baseUrl;
        public ProductService(HttpClient httpClient, IOptions<Settings> settings)
        {
            _client = httpClient;
            _settings = settings;
            //base url for product service
            _baseUrl = $"{_settings.Value.ProductApi}/api/product";
        }
        public async Task<ProductViewModel>GetProduct(Guid id)
        {
            var api = $"{_baseUrl}/{id}";
            var response = await _client.GetStringAsync(api);
            if (!string.IsNullOrEmpty(response))
            {
                var vm= JsonConvert.DeserializeObject<ProductViewModel>(response);
                return vm;
            }
            else
            {
                return new ProductViewModel { ProductId = id };
            }
        }
        public async Task<List<ProductViewModel>> ListProducts()
        {
            var api = $"{_baseUrl}/productlist";
            var response = await _client.GetStringAsync(api);
            return (!string.IsNullOrEmpty(response))
                ? JsonConvert.DeserializeObject<List<ProductViewModel>>(response)
                : new List<ProductViewModel>();
        }
    }
}
