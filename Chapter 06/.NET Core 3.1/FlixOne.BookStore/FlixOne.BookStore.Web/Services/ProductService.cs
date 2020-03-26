using FlixOne.BookStore.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FlixOne.BookStore.Web.Services
{

    public class ProductService:IProductService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient _httpClient;
        private readonly string _productList = string.Empty;
        private readonly string _baseURI = string.Empty;
        private readonly ITokenAcquisition _tokenAcquisition;

        public ProductService(ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _tokenAcquisition = tokenAcquisition;
            _contextAccessor = contextAccessor;
            _productList = configuration["ProductList:ProductListResourceId"];
            _baseURI = configuration["ProductList:ProductListBaseAddress"];
        }
        public async Task<IEnumerable<Product>> GetAsync()
        {
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { _productList });
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _httpClient.GetAsync($"{ _baseURI}api/v1/Product/productlist");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Product>>(content);
            }

            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
        }
    }
}
