using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using FlixOne.BookStore.Client.Models;
using Newtonsoft.Json;

namespace FlixOne.BookStore.Client
{
    public class ProductApiClient : BaseClient
    {
        public ProductApiClient(string baseUri) : base(baseUri)
        {}

        public ServiceStatus ApiStatus()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/echo/status");
            request.Headers.Add("Accept", "application/json");

            var response = HttpClient.SendAsync(request);

            try
            {
                var result = response.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<ServiceStatus>(result.Content.ReadAsStringAsync().Result,
                        JsonSettings);
                }

                ResponseError(request, result);
            }
            finally
            {
                Dispose(request, response);
            }

            return new ServiceStatus();
        }

        public IEnumerable<Product> GetProducts()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/product/productlist");
            request.Headers.Add("Accept", "application/json");
            var response = HttpClient.SendAsync(request);
            try
            {
                var result = response.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Product>>(
                        result.Content.ReadAsStringAsync().Result, JsonSettings);

                }

                ResponseError(request, result);
            }
            finally
            {
                Dispose(request, response);
            }

            return new List<Product>();
        }
    }
}