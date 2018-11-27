using System;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FlixOne.BookStore.Client
{
    public class BaseClient:IDisposable
    {
        public BaseClient(string baseUri)
        {
            HttpClient = new HttpClient { BaseAddress = new Uri(baseUri ?? "http://api.flixonebookstore.com/product/v2/capture") };
        }
        protected readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        protected HttpClient HttpClient;

        protected void ResponseError(HttpRequestMessage failedRequest, HttpResponseMessage failedResponse)
        {
            throw new HttpRequestException(
                $"The Product API request for {failedRequest.Method.ToString().ToUpperInvariant()} {failedRequest.RequestUri} failed. Response Status: {(int)failedResponse.StatusCode}, Response Body: {failedResponse.Content.ReadAsStringAsync().Result}");
        }
        public void Dispose()
        {
            Dispose(HttpClient);
        }
        
        public void Dispose(params IDisposable[] disposables)
        {
            foreach (var disposable in disposables.Where(disposable => disposable != null))
            {
                disposable.Dispose();
            }
        }
    }
}
