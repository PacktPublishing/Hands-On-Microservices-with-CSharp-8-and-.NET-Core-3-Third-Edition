using FlixOne.BookStore.Web.Config;
using FlixOne.BookStore.Web.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlixOne.BookStore.Web.Services
{
    public class OfferService : IOfferService
    {
        private readonly HttpClient _client;
        private readonly IOptions<Settings> _settings;
        private readonly string _baseUrl;
        public OfferService(HttpClient httpClient, IOptions<Settings> settings)
        {
            _client = httpClient;
            _settings = settings;
            //base url for offer service
            _baseUrl = $"{_settings.Value.ProductApi}/api/offer";
        }
        public async Task<List<OfferViewModel>> ListOffers(Guid dealId, Guid vendorId)
        {
            var api = $"{_baseUrl}/{dealId}/{vendorId}";
            var response = await _client.GetStringAsync(api);
            if (!string.IsNullOrEmpty(response))
            {
                return JsonConvert.DeserializeObject<List<OfferViewModel>>(response);
            }
            else
            {
                return new List<OfferViewModel>();
            }
        }
    }
}
